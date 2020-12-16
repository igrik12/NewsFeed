using NewsFeed.Actors;
using NewsFeed.Classes;

namespace NewsFeed.Factories
{
    public abstract class RequestFactory
    {
        public abstract Request CreateRequest();
    }
}