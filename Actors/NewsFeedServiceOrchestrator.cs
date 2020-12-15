using System;
using Akka.Actor;
using NewsAPI;
using NewsFeed.Messages;

namespace NewsFeed.Actors
{
    public class NewsFeedServiceOrchestrator : ReceiveActor
    {
        private readonly IActorRef _signalRActor;
        private readonly NewsApiClient _client;

        public NewsFeedServiceOrchestrator(IActorRef signalRActor, NewsApiClient client)
        {
            _signalRActor = signalRActor;
            _client = client;
            Begin();
        }

        private void Begin()
        {
            Receive<CreateService>(service =>
            {
                var requestType = service.Request.GetType();
                var actor = Context.ActorOf(Props.Create(() => (ReceiveActor)Activator.CreateInstance(typeof(NewsServiceActor<>).MakeGenericType(requestType), _client)), service.Request.EverythingRequest.Q);
                actor.Tell(service.Request);
                _signalRActor.Tell(new SendNewsServiceActor(actor), Self);
            });
        }
    }

}
