using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Kazoobot.Modules
{
    public class Kazoo : ModuleBase<SocketCommandContext>
    {
        [Command("kazoo"), Alias("K")]
        public async Task kazooooo(SocketUser User)
        {
            await ReplyAsync("Kazzo'd "+User.Username);

            var DMs = await User.GetOrCreateDMChannelAsync();

            await DMs.SendMessageAsync("https://www.youtube.com/watch?v=oPqigBVGpOE");
        }
        [Command("kazoo"), Alias("K")]
        public async Task kazoooo()
        {
            await ReplyAsync("https://www.youtube.com/watch?v=oPqigBVGpOE");
        }
        [Command("suc")]
        public async Task succccc()
        {
            await ReplyAsync("SUCK");
        }

    } 
}  
