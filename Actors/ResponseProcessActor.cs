using System;
using System.Collections.Generic;
using Akka.Actor;
using NewsAPI.Models;
using NewsAPI.Constants;

namespace NewsFeed.Actors
{
    public class ResponseProcessActor : ReceiveActor
    {

        public ResponseProcessActor()
        {
            Process();
        }

        private void Process()
        {
            Receive<List<Article>>(response =>
            {
                Console.WriteLine(response);
            });
        }
    }
}
