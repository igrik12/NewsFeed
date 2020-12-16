using NewsAPI.Models;

namespace NewsFeed.Classes
{
    public class GoogleRequest : Request
    {
        public GoogleRequest(TopHeadlinesRequest topHeadlinesRequest) : base(topHeadlinesRequest)
        {
        }
    }
}