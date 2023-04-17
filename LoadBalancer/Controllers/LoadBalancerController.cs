using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using RestSharp;


namespace LoadBalancer.Controllers;

[ApiController]
[Route("LB")]
public class LoadBalancerController : ControllerBase 
{
    private readonly RestClient _restClient = new();
    private readonly ILoadBalancer _loadBalancer = LoadBalancer.LoadBalancer.GetInstance();

    [HttpGet]
    [Route("SearchQuery")]
    public async Task<ActionResult<string>> Get(string query, int maxAmount)
    {
        var result = await CallService<string>("/search/" + query + "/" + maxAmount, Method.Get);
        return result;
    }
    
    [HttpPost]
    [Route("CallService")]
    public async Task<ActionResult<object>> Post([FromQuery] string query, [FromQuery] int maxAmount)
    {
        return await CallService<object>("/search/" + query + "/" + maxAmount, Method.Post);
    }
    
    private async Task<ActionResult<TResult>> CallService<TResult>(
        string url, Method method
    )
    {
        var service = _loadBalancer.NextService();
        if (service == null)
        {
            return StatusCode(503);
        }
    
        var result = await _restClient.ExecuteAsync<TResult>(
            new RestRequest(service.Url + url), method
        );
        
        int statusCode = (int)result.StatusCode;
        if (statusCode is 0 or >= 500)
        {
            Console.WriteLine("Service at URL " + service.Url + "returned status code " + (int)result.StatusCode + " and will be removed.");
            _loadBalancer.RemoveService(service.Id);
            return await CallService<TResult>(url, method);
        }

        return Ok(result.Data);
    }


}
