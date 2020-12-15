using Akka.Actor;
using System.Net.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsFeed.Classes;

namespace NewsFeed.Actors
{
    public class NewsServiceActor : ReceiveActor
    {
        public class FetchNews
        {
            public NewsFeed NewsFeed { get; }

            public FetchNews(NewsFeed newsFeed)
            {
                NewsFeed = newsFeed;
            }
        }
        private readonly HttpClient _httpClient;
        private readonly IActorRef _responseProcessActor;
        private readonly NewsFeed _newsFeed;

        public NewsServiceActor(HttpClient httpClient, IActorRef responseProcessActor, NewsFeed newsFeed)
        {
            _httpClient = httpClient;
            _responseProcessActor = responseProcessActor;
            _newsFeed = newsFeed;
            Service();
        }

        public NewsServiceActor()
        {
            Service();
        }

        private void Service()
        {
            Receive<FetchNews>((fetch =>
            {
                var newsApiClient = new NewsApiClient("9a610f9cfb9544e78333ae6ec536d17b");

                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = fetch.NewsFeed.ToString(),
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = new DateTime(2020, 11, 15),
                });
                Console.WriteLine(articlesResponse);
            }));
        }
    }
}
