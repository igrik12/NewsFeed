using NewsAPI.Models;
using NewsFeed.Classes;

namespace NewsFeed.Actors
{
    public interface IRequest
    {
        EverythingRequest EverythingRequest { get; }
    }

    public class GoogleRequest : Request
    {
        public GoogleRequest(EverythingRequest everythingRequest) : base(everythingRequest)
        {
        }
    }

    public class AppleRequest : Request
    {
        public AppleRequest(EverythingRequest everythingRequest) : base(everythingRequest)
        {
        }
    }

    public abstract class Request : IRequest
    {
        public EverythingRequest EverythingRequest { get; }

        protected Request(EverythingRequest everythingRequest)
        {
            EverythingRequest = everythingRequest;
        }

    }

    public abstract class RequestFactory
    {
        public abstract Request CreateRequest();
    }

    public class AppleRequestFactory : RequestFactory
    {
        private readonly RequestConfiguration _configuration;

        public AppleRequestFactory(RequestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Request CreateRequest()
        {
            return new AppleRequest(new EverythingRequest()
            {
                Q = _configuration.Q,
                SortBy = _configuration.SortBy,
                Language = _configuration.Language,
                From = _configuration.From
            });
        }
    }

    public class GoogleRequestFactory : RequestFactory
    {
        private readonly RequestConfiguration _configuration;

        public GoogleRequestFactory(RequestConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Request CreateRequest()
        {
            return new AppleRequest(new EverythingRequest()
            {
                Q = _configuration.Q,
                SortBy = _configuration.SortBy,
                Language = _configuration.Language,
                From = _configuration.From
            });
        }
    }
}