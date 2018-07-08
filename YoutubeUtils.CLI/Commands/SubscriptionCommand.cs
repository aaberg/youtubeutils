using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using YoutubeUtils.Tools;

namespace YoutubeUtils.CLI.Commands
{
    public class SubscriptionCommand : ISubscriptionCommand
    {
        private readonly ISubscriptionService _subscriptionService;
        
        public SubscriptionCommand(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        
        public void Configure(CommandLineApplication cmd)
        {
            cmd.Description = "Retrieve and manages your Youtube subscriptions";
            cmd.HelpOption("-?|-h|--help");
            
            var idOption = cmd.Option("-i|--id", "--id=[subscription id] - If this is not specified, all your subscriptions will be retrieved", CommandOptionType.SingleValue);

            var deleteOption = cmd.Option("-d|--delete", "--delete - Delete retrieved subscription(s)", CommandOptionType.NoValue);
            
            cmd.OnExecute(() =>
            {
                Console.WriteLine("Listing your subscriptions..");
                Console.WriteLine("If you are not already logged in, a browser will open to allow you to log in to Youtube");
                Console.WriteLine("");

                List<Subscription> subscriptions;
                if (idOption.HasValue())
                {
                    subscriptions = new List<Subscription> {_subscriptionService.GetSubscription(idOption.Value()).Result };
                }
                else
                {
                    subscriptions = _subscriptionService.GetSubscriptions().Result;
                }

                if (deleteOption.HasValue())
                {
                    Console.WriteLine($"This will delete {subscriptions.Count} subscriptions.");
                    Console.WriteLine("Type 'd' or 'delete' to confirm:");
                    var userInput = Console.ReadLine();
                    if (userInput.ToLower() == "delete" || userInput.ToLower() == "d")
                    {
                        subscriptions.ForEach(subscription =>
                        {
                            Console.Write($"Deleting subscription with id {subscription.Id}... ");
                            _subscriptionService.DeleteSubscription(subscription.Id);
                            Console.Write("OK\n");
                        });
                        
                    }
                    else
                    {
                        Console.WriteLine("Deletion canceled.");
                    }
                }
                else
                {
                    // list subscriptions
                    subscriptions.ForEach(subscription =>
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(subscription, Formatting.Indented));
                        Console.WriteLine("");
                    });
                    
                    Console.WriteLine($"\n\n--------------------\nFinished listing {subscriptions.Count} subscriptions");
                }
           
                return 0;
            });
        }
    }
}