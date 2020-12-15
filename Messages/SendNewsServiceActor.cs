using Akka.Actor;

namespace NewsFeed.Messages
{
    public class SendNewsServiceActor
    {
        public IActorRef Actor { get; }
        public SendNewsServiceActor(IActorRef actor)
        {
            Actor = actor;
        }
    }
}