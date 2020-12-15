using NewsFeed.Actors;

namespace NewsFeed.Messages
{
    public class CreateService
    {
        public IRequest Request { get; set; }

        public CreateService(IRequest request)
        {
            Request = request;
        }
    }
}