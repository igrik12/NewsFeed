using System.Net.Http;
using Akka.Actor;
using NewsFeed.Actors;

namespace NewsFeed.Classes
{
    public class AkkaStartupTasks
    {
        private readonly HttpClient _client;
        private readonly NewsFeedConfiguration _configuration;

        public AkkaStartupTasks(HttpClient client, NewsFeedConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public ActorSystem StartAkka()
        {
            SystemActors.ActorSystem = ActorSystem.Create("NewsFeedSystem");
            var signalRActor = SystemActors.SignalRActor = SystemActors.ActorSystem.ActorOf(Props.Create(() => new SignalRActor()), "signalr");
            SystemActors.NewsFeedOrchestratorActor = SystemActors.ActorSystem.ActorOf(Props.Create(() => new NewsFeedServiceOrchestrator(signalRActor)));
            //var responseProcessor = SystemActors.ResponseProcessActor =
            //    SystemActors.ActorSystem.ActorOf(Props.Create(() => new ResponseProcessActor()));
            //var newsService = SystemActors.NewsServiceActor = SystemActors.ActorSystem.ActorOf(Props.Create(() => new NewsServiceActor(_client, _configuration, responseProcessor)),
            //"commands");
            return SystemActors.ActorSystem;
        }
    }
}