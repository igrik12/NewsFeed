using System;
using System.Collections.Generic;
using Akka.Actor;
using NewsAPI.Models;
using NewsAPI.Constants;
using NewsFeed.Classes;
using NewsFeed.Messages;

namespace NewsFeed.Actors
{
    public class ResponseProcessActor : ReceiveActor
    {
        private readonly IActorRef _newsServiceActor;
        private readonly ActorSelection _signalRActor;
        private int _retryCount = 0;

        public ResponseProcessActor(IActorRef newsServiceActor)
        {
            _newsServiceActor = newsServiceActor;
            _signalRActor = Context.ActorSelection("akka://NewsFeedSystem/user/signalRActor");
            Process();
        }

        private void Process()
        {
            Receive<NewsResult>(result =>
            {
                if (result.Result.Error != null)
                {
                    if (result.Result.Error.Code == ErrorCodes.RequestTimeout && _retryCount <= 3)
                    {
                        _retryCount++;
                        _newsServiceActor.Tell(new FetchNews());
                    }
                    _signalRActor.Tell(result.Result.Error);
                }
                _signalRActor.Tell(result);
            });
        }
    }
}
