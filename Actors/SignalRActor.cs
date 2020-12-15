using System.Collections.Generic;
using Akka.Actor;
using NewsFeed.Classes;

namespace NewsFeed.Actors
{
    public class SignalRActor : ReceiveActor, IWithUnboundedStash
    {
        public class SetHub
        {
            public NewsFeedServiceHelper Hub { get; }

            public SetHub(NewsFeedServiceHelper hub)
            {
                Hub = hub;
            }
        }

        private NewsFeedServiceHelper _hub;
        private Dictionary<NewsFeed, IActorRef> _feedActorCache;

        public SignalRActor()
        {
            WaitingForHub();
        }

        private void WaitingForHub()
        {
            Receive<SetHub>(h =>
            {
                _hub = h.Hub;
                Become(HubAvailable);
                Stash.UnstashAll();
            });

            ReceiveAny(_ => Stash.Stash());
        }

        public IStash Stash { get; set; }

        private void HubAvailable()
        {
            Receive<Dictionary<NewsFeed, IActorRef>>(feedActorCache =>
            {
                _feedActorCache ??= feedActorCache;
            });
            Receive<NewsServiceActor.FetchNews>(fetch =>
            {
                if (_feedActorCache.TryGetValue(fetch.NewsFeed, out var actor))
                {
                    actor.Tell(new NewsServiceActor.FetchNews(fetch.NewsFeed));
                }
            });
        }

    }
}
