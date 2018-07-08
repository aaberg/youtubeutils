using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YoutubeUtils.Tools
{
    public class CredentialHelper
    {
        public static async Task<UserCredential> RetrieveCredential()
        {
            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                var googleClientSecrets = GoogleClientSecrets.Load(stream);
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    googleClientSecrets.Secrets,
                    new[] {YouTubeService.Scope.Youtube},
                    "user",
                    CancellationToken.None,
                    new FileDataStore("YoutubeUtils")
                );
            }
        }
    }
}