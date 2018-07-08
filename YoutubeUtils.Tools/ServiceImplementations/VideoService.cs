using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools.ServiceImplementations
{
    public class VideoService : BaseYoutubeService, IVideoService
    {
        public VideoService(IConfiguration configuration) : base(configuration)
        {
        }
        
        public async Task<List<Video>> GetRatedVideos(Rating rating)
        {
            var service = await GetYouTubeService();

            var request = service.Videos.List("snippet");
            request.MyRating = mapRating(rating);

            var videos = new List<Video>();

            do
            {
                var response = await request.ExecuteAsync();

                videos.AddRange(response.Items);

                request.PageToken = response.NextPageToken;
            } while (request.PageToken != null);

            return videos;
        }

        private VideosResource.ListRequest.MyRatingEnum mapRating(Rating rating)
        {
            switch (rating)
            {
                case Rating.Like:
                    return VideosResource.ListRequest.MyRatingEnum.Like;
                case Rating.Dislike:
                    return VideosResource.ListRequest.MyRatingEnum.Dislike;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}