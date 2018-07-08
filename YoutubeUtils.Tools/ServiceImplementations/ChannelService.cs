using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools.ServiceImplementations
{
    public class ChannelService: BaseYoutubeService, IChannelService

    {
        public ChannelService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Channel> GetChannel(string id)
        {
            var service = await GetYouTubeService();

            var request = service.Channels.List("snippet");
            request.Id = id;
                
            var response = await request.ExecuteAsync();

            return response.Items.Count >= 1 ? response.Items[0] : null;
        }
    }
}