using NewsFeed.Actors;
using NewsFeed.Classes;

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