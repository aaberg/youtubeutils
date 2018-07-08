using Microsoft.Extensions.CommandLineUtils;
using YoutubeUtils.CLI.Commands;

namespace YoutubeUtils.CLI
{
    public class CommandLineApplicationInitializer : ICommandLineApplicationInitializer
    {
        private readonly ISubscriptionCommand _subscriptionCommand;
        private readonly IChannelCommand _channelCommand;
        private readonly ILikedVideoCommand _likedVideoCommand;
        
        public CommandLineApplicationInitializer(ISubscriptionCommand subscriptionCommand, IChannelCommand channelCommand, ILikedVideoCommand likedVideoCommand)
        {
            _subscriptionCommand = subscriptionCommand;
            _channelCommand = channelCommand;
            _likedVideoCommand = likedVideoCommand;
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
            cmd.Command("liked-videos", application => _likedVideoCommand.Configure(application));

            return cmd;
        }
    }
}