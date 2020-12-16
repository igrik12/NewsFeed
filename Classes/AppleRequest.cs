using NewsAPI.Models;
using NewsFeed.Actors;

namespace NewsFeed.Classes
{
    public class AppleRequest : Request
    {
        public AppleRequest(TopHeadlinesRequest topHeadlinesRequest) : base(topHeadlinesRequest)
        {
        }
    }
}