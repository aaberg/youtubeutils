using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools
{
    public interface IVideoService
    {
        Task<List<Video>> GetRatedVideos(Rating rating);
    }
}