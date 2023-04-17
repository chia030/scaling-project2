using Common;
using ConsoleSearch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using RestSharp;

namespace SearchAPI.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {   

        // bool LoadBalancerConnector()
        // {
        //     var restClient = new RestClient("http://load-balancer"); // or http://localhost:9000/
        //     var response = restClient.Post(
        //         new RestRequest(
        //             "Configuration?url=http://" + Environment.MachineName, Method.Post
        //         )
        //     );
        //     if(!response.IsSuccessful)
        //     {
        //         return false;
        //     }

        // var retryCount = 0;
        // while(!LoadBalancerConnector() && retryCount < 3)
        // {
        //     Thread.Sleep(2000);
        //     retryCount++;
        // }

        //     Console.WriteLine("Service registered succesfully.");
        //     return true;
        // }



        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public async Task<string> Search(string query, int maxAmount)
        {
            var mSearchLogic = new SearchLogic(new Database());
            var result = new SearchResult();

            var wordIds = new List<int>();
            var searchTerms = query.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in searchTerms)
            {
                int id = mSearchLogic.GetIdOf(t);
                if (id != -1)
                {
                    wordIds.Add(id);
                }
                else
                {
                    result.IgnoredTerms.Add(t);
                }
            }

            DateTime start = DateTime.Now;

            var docIds = await mSearchLogic.GetDocuments(wordIds);

            // get details for the first 10             
            var top = new List<int>();
            foreach (var p in docIds.GetRange(0, Math.Min(maxAmount, docIds.Count)))
            {
                top.Add(p.Key);
            }

            result.ElapsedMilliseconds = (DateTime.Now - start).TotalMilliseconds;

            int idx = 0;
            foreach (var doc in await mSearchLogic.GetDocumentDetails(top))
            {
                result.Documents.Add(new Document{Id = idx+1,Path = doc, NumberOfOccurences = docIds[idx].Value});
                idx++;
            }

            Console.WriteLine("Search API fetching data");

            //string resultJson = JsonSerializer.Serialize<SearchResult>(result);
            var resultStr = JsonConvert.SerializeObject(result, Formatting.Indented);
            return resultStr;

        }
    }
}
