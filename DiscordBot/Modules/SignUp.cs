using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
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
