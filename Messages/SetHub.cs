using NewsFeed.Classes;

namespace NewsFeed.Messages
{
    public class SetHub
    {
        public NewsFeedServiceHelper Hub { get; }

        public SetHub(NewsFeedServiceHelper hub)
        {
            Hub = hub;
        }
    }
}