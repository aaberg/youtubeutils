using System;
using System.Collections.Generic;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.CommandLineUtils;
using YoutubeUtils.Tools;

namespace YoutubeUtils.CLI.Commands
{
    public class LikedVideoCommand : ILikedVideoCommand
    {
        private readonly IVideoService _videoService;

        public LikedVideoCommand(IVideoService videoService)
        {
            _videoService = videoService;
        }
        
        public void Configure(CommandLineApplication cmd)
        {
            cmd.Description = "Retrieves and manages videos that you have rated with 'like'";
            cmd.HelpOption("-?|-h|--help");
            
            cmd.OnExecute(() =>
            {
                var videos = _videoService.GetRatedVideos(Rating.Like).Result;
                
                videos.ForEach(video =>
                {
                    Console.WriteLine(video.Snippet.Title);
                });

                return 0;
            });
        }
    }
}