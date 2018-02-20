using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotCore.Modules
{
    public class SignUp : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task SignUpAsync()
        {

            await ReplyAsync($"{Context.User.Mention}, you've been added to the DB!");
        }
    }
}
