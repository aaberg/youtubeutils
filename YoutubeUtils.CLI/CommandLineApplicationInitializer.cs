using Microsoft.Extensions.CommandLineUtils;
using YoutubeUtils.CLI.Commands;

namespace YoutubeUtils.CLI
{
    public class CommandLineApplicationInitializer : ICommandLineApplicationInitializer
    {
        private readonly ISubscriptionCommand _subscriptionCommand;
        private readonly IChannelCommand _channelCommand;
        
        public CommandLineApplicationInitializer(ISubscriptionCommand subscriptionCommand, IChannelCommand channelCommand)
        {
            _subscriptionCommand = subscriptionCommand;
            _channelCommand = channelCommand;
        }
        
        public CommandLineApplication Initializer()
        {
            var cmd = new CommandLineApplication();

            cmd.Name = "YoutubeUtils";
            cmd.Description = "Some handy utilities for Youtube, by Lars Aaberg";

            cmd.HelpOption("-?|-h|--help");

            cmd.VersionOption("-v|--version", () => "Version 1.0");
            
            cmd.OnExecute(() =>
            {
                cmd.ShowHint();

                return 0;
            });
            
            
            // setup commands
            cmd.Command("subscription", application => _subscriptionCommand.Configure(application));
            cmd.Command("channel", application => _channelCommand.Configure(application));

            return cmd;
        }
    }
}