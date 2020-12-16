using NewsAPI.Models;
using NewsFeed.Actors;

namespace NewsFeed.Classes
{
    public class GoogleRequest : Request
    {
        public GoogleRequest(EverythingRequest everythingRequest) : base(everythingRequest)
        {
        }
    }
}