using Akka.Actor;
using NewsAPI;
using NewsAPI.Models;
using NewsFeed.Classes;
using NewsFeed.Messages;

namespace NewsFeed.Actors
{
    public class NewsServiceActor<T> : ReceiveActor where T : IRequest
    {
        private readonly IActorRef _responseProcessActor;
        private readonly NewsApiClient _client;
        private EverythingRequest _everythingRequest;

        public NewsServiceActor(NewsApiClient client)
        {
            _responseProcessActor = Context.ActorOf(Props.Create(() => new ResponseProcessActor(Self)));
            _client = client;
            Service();
        }

        private void Service()
        {
            Receive<T>((msg =>
            {
                _everythingRequest = msg.EverythingRequest;
            }));

            Receive<FetchNews>(fetch =>
            {
                _client.GetEverythingAsync(_everythingRequest).ContinueWith<NewsResult>((res) => new NewsResult(_everythingRequest.Q, res.Result)).PipeTo(_responseProcessActor);
            });
        }
    }
}
