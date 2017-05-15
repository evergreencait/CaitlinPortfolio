using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaitlinPortfolio.Models
{
    [Table("Projects")]
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string html_url { get; set; }

        public static List<Project> GetProjects()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/evergreencait/starred", Method.GET);
            request.AddHeader("Accept", "application / vnd.github.v3 + json");
            request.AddHeader("User-Agent", "evergreencait");
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            Console.WriteLine(jsonResponse);
            string jsonOutput = jsonResponse.ToString();
            var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonOutput);
            Console.WriteLine(projectList[0].Name);
            return projectList;
        }


        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }

}
