using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YoutubeUtils.Tools.ServiceImplementations
{
    public class BaseYoutubeService
    {
        private readonly IConfiguration _configuration;

        public BaseYoutubeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task<YouTubeService> GetYouTubeService()
        {
            var credential = await CredentialHelper.RetrieveCredential();

            return new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _configuration.ApplicationName
            });
        }
    }
}