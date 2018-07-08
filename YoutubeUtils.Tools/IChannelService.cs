using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools
{
    public interface IChannelService
    {
        Task<Channel> GetChannel(string id);
    }
}