using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using Microsoft.AspNetCore.SignalR;
using NewsAPI.Models;
using NewsFeed.Classes;
using NewsFeed.Messages;

namespace NewsFeed.Actors
{
    public class SignalRActor : ReceiveActor, IWithUnboundedStash
    {
        private NewsFeedServiceHelper _hub;
        private readonly Dictionary<string, IActorRef> _feedActorCache;
        public IStash Stash { get; set; }

        public SignalRActor()
        {
            _feedActorCache = new Dictionary<string, IActorRef>();
            WaitingForHub();
        }

        private void WaitingForHub()
        {
            Receive<SetHub>(h =>
            {
                _hub = h.Hub;
                Become(HubAvailable);
                Stash.UnstashAll();
            });

            ReceiveAny(_ => Stash.Stash());
        }


        private void HubAvailable()
        {
            Receive<SendNewsServiceActor>(send =>
            {
                if (!_feedActorCache.ContainsKey(send.Actor.Path.Name))
                {
                    _feedActorCache.Add(send.Actor.Path.Name, send.Actor);
                }
            });
            Receive<FetchNews>(fetch =>
            {
                foreach (var kvp in _feedActorCache)
                {
                    kvp.Value.Tell(new FetchNews());
                }
            });

            Receive<NewsResult>((result) => _hub.NewsHub.Clients.All.SendAsync("ArticlesResult", result));
        }

    }
}
