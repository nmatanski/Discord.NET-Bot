using DAL.Database;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserService;
using UserService.Domain.Models;

namespace DiscordBot.Modules.Admin_Modules
{
    public class ChangeRole : ModuleBase<SocketCommandContext>
    {
        UserManager userManager = new UserManager();

        [Command("give role")]
        public async Task GiveRoleAsync(string mention, string newRole)
        {
            var mentionedUser = Context.Guild.Users.FirstOrDefault(x => "<@" + x.Id + ">" == mention);
            if (mentionedUser == null)
            {
                await ReplyAsync($"{Context.User.Mention}, {mention} does not exist. Please try again.");
                return;
            }

            var dbUser = await userManager.GetUserByUsernameAsync(mentionedUser.Username);
            Enum.TryParse(newRole, out Role tempRole);
            dbUser.UserRole = tempRole;
            AppDbContext db = new AppDbContext();
            db.SaveChanges();

            var user = Context.User as IGuildUser;
            var userRoles = user.Guild.Roles;
            if (!userRoles.Contains(userRoles.FirstOrDefault(x => x.Name == "Administrator")))
            {
                return;
            }

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == newRole);
            if(role == null)
            {
                await ReplyAsync($"{Context.User.Mention}, {newRole} does not exist. Please try again.");
                return;
            }

            await (mentionedUser as IGuildUser).AddRoleAsync(role);
        }

        [Command("remove role")]
        public async Task RemoveRoleAsync(string mention, string newRole)
        {
            var mentionedUser = Context.Guild.Users.FirstOrDefault(x => "<@" + x.Id + ">" == mention);
            if (mentionedUser == null)
            {
                await ReplyAsync($"{Context.User.Mention}, {mention} does not exist. Please try again.");
                return;
            }
            var user = Context.User as IGuildUser;
            var userRoles = user.Guild.Roles;
            if (!userRoles.Contains(userRoles.FirstOrDefault(x => x.Name == "Administrator")))
            {
                return;
            }

            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == newRole);
            if (role == null)
            {
                await ReplyAsync($"{Context.User.Mention}, {newRole} does not exist. Please try again.");
                return;
            }

            await (mentionedUser as IGuildUser).RemoveRoleAsync(role);
        }
    }
}
