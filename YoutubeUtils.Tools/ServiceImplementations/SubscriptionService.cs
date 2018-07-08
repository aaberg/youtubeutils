using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools.ServiceImplementations
{
    public class SubscriptionService : BaseYoutubeService, ISubscriptionService
    {
        public SubscriptionService(IConfiguration configuration) : base(configuration)
        {}

        public async Task<List<Subscription>> GetSubscriptions()
        {
            var service = await GetYouTubeService();

            var listRequest = service.Subscriptions.List("snippet");
            listRequest.Mine = true;

            var subscriptions = new List<Subscription>();

            do
            {
                var subscriptionListResponse = await listRequest.ExecuteAsync();

                foreach (Subscription s in subscriptionListResponse.Items)
                {
                    subscriptions.Add(s);
                }

                listRequest.PageToken = subscriptionListResponse.NextPageToken;

            } while (listRequest.PageToken != null);

            return subscriptions;
        }

        public async Task<Subscription> GetSubscription(string id)
        {
            var service = await GetYouTubeService();

            var listRequest = service.Subscriptions.List("snippet");
            listRequest.Id = id;

            var response = await listRequest.ExecuteAsync();

            return response.Items.Count >= 1 ? response.Items[0] : null;
        }

        public async Task DeleteSubscription(string id)
        {
            var service = await GetYouTubeService();

            var deleteRequest = service.Subscriptions.Delete(id);

            await deleteRequest.ExecuteAsync();
        }
    }
}