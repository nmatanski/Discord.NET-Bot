using Discord.Commands;
using System.Threading.Tasks;
using UserService;
using UserService.Domain.Models;

namespace DiscordBot.Modules
{
    public class Signup : ModuleBase<SocketCommandContext>
    {
        UserManager userManager = new UserManager();

        [Command("signup")]
        public async Task SignUpAsync()
        {
            var tempUser = await userManager.GetUserByUsernameAsync(Context.User.Username);
            if (tempUser != null)
            {
                await ReplyAsync($"{Context.User.Mention}, you've already signed up with this account.");
                if(tempUser.Email.Equals("@"))
                    await ReplyAsync($"Please confirm your email address.");

                return;
            }

            await userManager.RegisterAsync(new User(Context.User.Username, "@", "", Role.User));

            await ReplyAsync($"{Context.User.Mention}, you've been added to the DB!");

            await Discord.UserExtensions.SendMessageAsync(Context.Message.Author, "Sign up link: https://discordbot-web.azurewebsites.net/Account/Register");
        }
    }
}
