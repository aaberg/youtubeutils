using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUtils.Tools
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetSubscriptions();

        Task<Subscription> GetSubscription(string id);

        Task DeleteSubscription(string id);
    }
}