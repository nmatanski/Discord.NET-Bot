using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotCore.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync($"{Context.User.Mention} Pong! /{(Context.Client as DiscordSocketClient).Latency}ms :ping_pong:");
        }
    }
}
