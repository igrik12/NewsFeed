using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NewsAPI.Constants;
using NewsFeed.Actors;
using NewsFeed.Factories;
using NewsFeed.Messages;

namespace NewsFeed.Classes
{
    public class NewsFeedServiceHelper : IHostedService
    {
        public readonly IHubContext<NewsHub> NewsHub;
        private readonly AkkaStartupTasks _akkaStartupTasks;
        private readonly NewsFeedConfiguration _configuration;

        public NewsFeedServiceHelper(IHubContext<NewsHub> newsHub, AkkaStartupTasks akkaStartupTasks, NewsFeedConfiguration configuration)
        {
            NewsHub = newsHub;
            _akkaStartupTasks = akkaStartupTasks;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _akkaStartupTasks.StartAkka();
            SystemActors.SignalRActor.Tell(new SetHub(this));
            _configuration.RequestConfigurations.ForEach(config => SystemActors.NewsFeedOrchestratorActor.Tell(new CreateService(CreateRequest(config))));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Request CreateRequest(RequestConfiguration configuration)
        {
            return configuration.Q switch
            {
                "Apple" => new AppleRequestFactory(configuration).CreateRequest(),
                "Google" => new GoogleRequestFactory(configuration).CreateRequest(),
                _ => new GoogleRequestFactory(new RequestConfiguration()
                {
                    Q = "Google",
                    Language = Languages.EN,
                    SortBy = SortBys.Popularity,
                    From = DateTime.Today
                }).CreateRequest()
            };
        }
    }
}
