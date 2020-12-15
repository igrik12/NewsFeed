using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using NewsFeed.Actors;

namespace NewsFeed
{
    public class NewsHub : Hub
    {

        public NewsHub()
        {
        }

        public void GetNews()
        {
            SystemActors.SignalRActor.Tell(new NewsServiceActor.FetchNews(Actors.NewsFeed.Google), ActorRefs.Nobody);
        }
    }
}
