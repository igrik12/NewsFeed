using System.Collections.Generic;
using Akka.Actor;

namespace NewsFeed.Actors
{
    public class NewsFeedServiceOrchestrator : ReceiveActor
    {
        public Dictionary<NewsFeed, IActorRef> ServiceCache { get; } = new Dictionary<NewsFeed, IActorRef>();
        public class CreateService
        {
            public NewsFeed NewsFeed { get; }

            public CreateService(NewsFeed newsFeed)
            {
                NewsFeed = newsFeed;
            }
        }
        private readonly IActorRef _signalRActor;

        public NewsFeedServiceOrchestrator(IActorRef signalRActor)
        {
            _signalRActor = signalRActor;
            Begin();
        }

        private void Begin()
        {
            Receive<CreateService>(service =>
            {
                if (!ServiceCache.ContainsKey(service.NewsFeed))
                {
                    ServiceCache.Add(service.NewsFeed, Context.ActorOf<NewsServiceActor>($"{service.NewsFeed.ToString()}newsFeed"));
                }
                _signalRActor.Tell(ServiceCache, Self);
            });
        }
    }
}
