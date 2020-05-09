using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SampleUniversity.Model;

namespace ConsoleApp1
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

    class Program
    {
        static async Task Main(string[] args)
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"avjgit:77bcc2d493e53801665125ac9da5b99589ddb700"));
            var httpClient = new HttpClient {BaseAddress = new Uri("https://api.github.com/graphql")};
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Test");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            var searchText = "andrejs";

            var queryObject = new
            {
                query = @"
{
  search(
    first: 1,
    query:""" + searchText + @""", 
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
}"};

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

            Console.WriteLine(responseObj.data.search.nodes.FirstOrDefault()?.Description);
            Console.ReadLine();
        }
    }
}
