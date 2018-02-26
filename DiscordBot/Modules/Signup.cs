using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
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
            await userManager.RegisterAsync(new User(Context.User.Username, "@", "", Role.User));

            await ReplyAsync($"{Context.User.Mention}, you've been added to the DB!");

            await Discord.UserExtensions.SendMessageAsync(Context.Message.Author, "sign up link with email, password and sending confirmation link");
        }
    }
}
