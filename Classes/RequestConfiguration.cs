using System;
using NewsAPI.Constants;

namespace NewsFeed.Classes
{
    public class RequestConfiguration
    {
        public string Q { get; set; }
        public SortBys SortBy { get; set; }
        public Languages Language { get; set; }
        public DateTime From { get; set; } = DateTime.Today;
    }
}
