using NewsAPI.Models;

namespace NewsFeed.Classes
{
    public interface IRequest
    {
        TopHeadlinesRequest TopHeadlinesRequest { get; }
    }

    public abstract class Request : IRequest
    {
        public TopHeadlinesRequest TopHeadlinesRequest { get; }

        protected Request(TopHeadlinesRequest topHeadlinesRequest)
        {
            TopHeadlinesRequest = topHeadlinesRequest;
        }
    }
}