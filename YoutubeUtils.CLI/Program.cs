using System;
using Microsoft.Extensions.DependencyInjection;
using YoutubeUtils.CLI.Commands;
using YoutubeUtils.Tools;
using YoutubeUtils.Tools.ServiceImplementations;

namespace YoutubeUtils.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // setting up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration, DefaultConfiguration>()
                
                // Configure services
                .AddSingleton<ISubscriptionService, SubscriptionService>()
                .AddSingleton<IChannelService, ChannelService>()
                .AddSingleton<IVideoService, VideoService>()
                
                // Configure commands
                .AddSingleton<ISubscriptionCommand, SubscriptionCommand>()
                .AddSingleton<IChannelCommand, ChannelCommand>()
                .AddSingleton<ILikedVideoCommand, LikedVideoCommand>()
                
                // Configure CommandLine Initializer
                .AddSingleton<ICommandLineApplicationInitializer, CommandLineApplicationInitializer>()
                
                // Build
                .BuildServiceProvider();


            var commandLineApplicationInitializer = serviceProvider.GetService<ICommandLineApplicationInitializer>();

            var cmd = commandLineApplicationInitializer.Initializer();

            cmd.Execute(args);
        }
    }
}