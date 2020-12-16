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
            return new GoogleRequest(new EverythingRequest()
            {
                Q = _configuration.Q,
                SortBy = _configuration.SortBy,
                Language = _configuration.Language,
                From = _configuration.From
            });
        }
    }
}