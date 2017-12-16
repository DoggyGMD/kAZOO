using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;


namespace Kazoobot.Modules
{
    public class greetings : ModuleBase<SocketCommandContext>
        {
        [Command("hi")]
        public async Task hi()
        {
            await ReplyAsync("hello!");
        }


    }
}
