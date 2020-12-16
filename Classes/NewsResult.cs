using NewsAPI.Models;

namespace NewsFeed.Classes
{
    public class NewsResult
    {
        public string Provider { get; }
        public ArticlesResult Result { get; }

        public NewsResult(string provider, ArticlesResult result)
        {
            Provider = provider;
            Result = result;
        }
    }
}