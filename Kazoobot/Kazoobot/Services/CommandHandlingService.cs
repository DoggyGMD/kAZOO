using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using LiteDB;

namespace DiscordBot.Services
{
    public class CommandHandlingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private IServiceProvider _provider;
        private LiteDatabase _database;

        public CommandHandlingService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, LiteDatabase database)
        {
            _discord = discord;
            _commands = commands;
            _provider = provider;
            _database = database;

            _discord.MessageReceived += MessageReceived;
            _discord.UserJoined += Join;
            _discord.UserLeft += Left;
         
            _discord.GuildAvailable += available;
            
        }

        private async Task available(SocketGuild X)
        {
            var general = X.TextChannels.Where(x => x.Name.ToLower().Contains("general") || x.Topic.ToLower().Contains("general")).FirstOrDefault();
            await general.SendMessageAsync("I live");
        }

       

        private async Task Left(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.GetTextChannel(391980073047293952);
            await channel.SendMessageAsync("User " + user.Mention + " left ther server!");
        }

        private async Task Join(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.GetTextChannel(391980073047293952);
            await channel.SendMessageAsync("Welcome " + user.Mention + "!");
        }


        public async Task InitializeAsync(IServiceProvider provider)
        {
            _provider = provider;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
            // Add additional initialization code here...
        }

        private async Task MessageReceived(SocketMessage rawMessage)
        {
            // Ignore system messages and messages from bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;
            
            int argPos = 0;
            //Uncomment below to enable a string prefix(these should only be used with private bots!)
             if (!message.HasStringPrefix("!", ref argPos)) return;

            var context = new SocketCommandContext(_discord, message);
            var result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (result.Error.HasValue &&
                result.Error.Value == CommandError.UnknownCommand)
                return;

            if (result.Error.HasValue && 
                result.Error.Value != CommandError.UnknownCommand)
                await context.Channel.SendMessageAsync(result.ToString());

        }
    }
}
