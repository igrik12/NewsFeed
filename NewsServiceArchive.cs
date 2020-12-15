using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NewsAPI.Models;

namespace NewsFeed
{

    public interface INewsService
    {
        Task<List<Article>> ReadNewsAsync();
    }

    public class NewsServiceArchive : INewsService
    {
        const string BestStoriesApi = "https://hacker-news.firebaseio.com/v0/beststories.json";
        const string StoryApiTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json";

        private HttpClient client;
        public NewsServiceArchive(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<List<Article>> ReadNewsAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BestStoriesApi),
                Headers =
                {
                    { "x-rapidapi-key", "94b02da124msha0348bb449686abp1654bbjsnbe18d5e60a63" },
                    { "x-rapidapi-host", "community-hacker-news-v1.p.rapidapi.com" },
                },
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var ids = JsonConvert.DeserializeObject<List<int>>(body);

            var articles = await GetArticlesAsync(ids);
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
            return articles;
        }

        private async Task<List<Article>> GetArticlesAsync(IEnumerable<int> ids)
        {
            var articles = new List<Article>();

            List<Task<Article>> downloadTasks = ids.Select(id => ProcessUrlAsync(string.Format(StoryApiTemplate, id), client)).ToList();

            while (downloadTasks.Any())
            {
                Task<Article> finishedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(finishedTask);
                articles.Add(await finishedTask);
            }

            return articles;
        }

        static async Task<Article> ProcessUrlAsync(string url, HttpClient client)
        {
            Article story = new Article();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var storyResponse = response.Content.ReadAsStringAsync().Result;
                story = JsonConvert.DeserializeObject<Article>(storyResponse);
            }
            else
            {
                story.Title = "ERROR RETRIEVING STORY";
            }
            return story;
        }
    }
}
