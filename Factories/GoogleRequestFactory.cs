using NewsAPI.Models;
using NewsFeed.Actors;
using NewsFeed.Classes;

namespace NewsFeed.Factories
{
    public class GoogleRequestFactory : RequestFactory
    {
        private readonly RequestConfiguration _configuration;

        public GoogleRequestFactory(RequestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Request CreateRequest()
        {
            return new GoogleRequest(new TopHeadlinesRequest
            {
                Q = _configuration.Q,
                Category = _configuration.Category,
                Language = _configuration.Language,
                Country = _configuration.Country,
            });
        }
    }
}