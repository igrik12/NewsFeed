using System.Collections.Generic;

namespace NewsFeed.Classes
{
    public class NewsFeedConfiguration
    {
        public List<RequestConfiguration> RequestConfigurations{ get; set; }
        public string Api { get; set; }
    }
}