using Akka.Actor;

namespace NewsFeed.Actors
{
    public static class SystemActors
    {
        public static ActorSystem ActorSystem;

        public static IActorRef SignalRActor = ActorRefs.Nobody;
        public static IActorRef NewsFeedOrchestratorActor = ActorRefs.Nobody;
    }
}