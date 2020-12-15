using Microsoft.AspNetCore.SignalR;
using Akka.Actor;
using NewsFeed.Actors;
using NewsFeed.Messages;

namespace NewsFeed
{
    public class NewsHub : Hub
    {
        public void GetNews()
        {
            SystemActors.SignalRActor.Tell(new FetchNews(),
                ActorRefs.Nobody);
        }
    }
}
