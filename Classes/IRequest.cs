using NewsAPI.Models;

namespace NewsFeed.Classes
{
    public interface IRequest
    {
        EverythingRequest EverythingRequest { get; }
    }

    public abstract class Request : IRequest
    {
        public EverythingRequest EverythingRequest { get; }

        protected Request(EverythingRequest everythingRequest)
        {
            EverythingRequest = everythingRequest;
        }
    }

    public enum RequestType
    {
        EverythingRequest,
        TopHeadlinesRequest
    }
}