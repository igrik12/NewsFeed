using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NewsFeed.Actors;

namespace NewsFeed.Classes
{
    public class NewsFeedServiceHelper : IHostedService
    {
        private readonly IHubContext<NewsHub> _hub;
        private readonly AkkaStartupTasks _akkaStartupTasks;
        private readonly NewsFeedConfiguration _configuration;

        public NewsFeedServiceHelper(IHubContext<NewsHub> hub, AkkaStartupTasks akkaStartupTasks, NewsFeedConfiguration configuration)
        {
            _hub = hub;
            _akkaStartupTasks = akkaStartupTasks;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _akkaStartupTasks.StartAkka();
            SystemActors.SignalRActor.Tell(new SignalRActor.SetHub(this));
            _configuration.Feeds.ForEach(feed => SystemActors.NewsFeedOrchestratorActor.Tell(new NewsFeedServiceOrchestrator.CreateService(Enum.Parse<Actors.NewsFeed>(feed))));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
