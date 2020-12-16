using NewsAPI.Constants;
using NewsAPI.Models;
using NewsFeed.Actors;
using NewsFeed.Classes;

namespace NewsFeed.Factories
{
    public class AppleRequestFactory : RequestFactory
    {
        private readonly RequestConfiguration _configuration;

        public AppleRequestFactory(RequestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Request CreateRequest()
        {
            return new AppleRequest(new TopHeadlinesRequest()
            {
                Q = _configuration.Q,
                Category = _configuration.Category,
                Language = _configuration.Language,
                Country = _configuration.Country,
            });
        }
    }
}