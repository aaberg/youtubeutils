using System;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using YoutubeUtils.Tools;

namespace YoutubeUtils.CLI.Commands
{
    public class ChannelCommand : IChannelCommand
    {
        private readonly IChannelService _channelService;

        public ChannelCommand(IChannelService channelService)
        {
            _channelService = channelService;
        }
        
        public void Configure(CommandLineApplication cmd)
        {
            cmd.Description = "Retrieves Youtube channels";
            cmd.HelpOption("-?|-h|--help");

            var idOption = cmd.Option("-id|--id", "--id=[channel id] - Sets the ID of the channel to receive.",
                CommandOptionType.SingleValue);
            
            cmd.OnExecute(() =>
            {
                if (!idOption.HasValue())
                {
                    Console.WriteLine("Channel ID must be specified. Use the --id=[Channel id] argument");
                    return 1;
                }

                var channel = _channelService.GetChannel(idOption.Value()).Result;
                
                Console.WriteLine(JsonConvert.SerializeObject(channel, Formatting.Indented));
                return 0;
            });
        }
    }
}