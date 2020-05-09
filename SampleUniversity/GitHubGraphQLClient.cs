using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SampleUniversity.Model;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SampleUniversity
{
    public class GraphQlApiSearchResult
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public Search search { get; set; }
    }
    public class Search
    {
        public List<Repository> nodes { get; set; }
    }

    public class GitHubGraphQLClient
    {
        public static async Task<Repository> GetRepositoryInfo(string searchQuery)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"avjgit:{token}}"));
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.github.com/graphql") };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Test");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            var queryObject = new
            {
                query = @"
{
  search(
    first: 1,
    query:""" + searchQuery + @""", 
    type: REPOSITORY)
  {
    nodes {
      ... on Repository {
            name
                description
                url
        }
    }
  }
}"
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json")
            };

            GraphQlApiSearchResult responseObj;

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                responseObj = JsonConvert.DeserializeObject<GraphQlApiSearchResult>(responseString);
            }

            return responseObj.data.search.nodes.FirstOrDefault();
        }
    }
}
