using NewsAPI.Constants;

namespace NewsFeed.Classes
{
    public class RequestConfiguration
    {
        public string Q { get; set; }
        public Categories Category { get; set; }
        public Languages Language { get; set; }
        public Countries Country { get; set; }
    }
}
