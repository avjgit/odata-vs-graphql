using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SampleUniversity.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleUniversity
{
    public class SearchResult
    {
        [JsonPropertyName("items")]
        public List<Repository> Items { get; set; }
    }

    public class GitHubODataClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<SearchResult> GetRepositoryInfo(string searchQuery)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "Test");
            var streamTask = Client.GetStreamAsync("https://api.github.com/search/repositories?q=" + searchQuery);
            return await JsonSerializer.DeserializeAsync<SearchResult>(await streamTask);
        }
    }
}
