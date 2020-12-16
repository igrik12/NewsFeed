using System.Net.Http;
using Akka.Actor;
using NewsAPI;
using NewsFeed.Actors;

namespace NewsFeed.Classes
{
    public class AkkaStartupTasks
    {
        private readonly NewsFeedConfiguration _configuration;
        private readonly NewsApiClient _client;

        public AkkaStartupTasks(NewsFeedConfiguration configuration, NewsApiClient client)
        {
            _configuration = configuration;
            _client = client;
        }
        public ActorSystem StartAkka()
        {
            SystemActors.ActorSystem = ActorSystem.Create("NewsFeedSystem");
            var signalRActor = SystemActors.SignalRActor = SystemActors.ActorSystem.ActorOf(Props.Create(() => new SignalRActor()), "signalRActor");
            SystemActors.NewsFeedOrchestratorActor = SystemActors.ActorSystem.ActorOf(Props.Create(() => new NewsFeedServiceOrchestrator(signalRActor, _client)));
            return SystemActors.ActorSystem;
        }
    }
}