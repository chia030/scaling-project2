using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RestSharp;
using JWTLoginAuthenticationAuthorization.Models;
using System.Reflection;

namespace WebSearch.Services
{
public class AuthenticateService : IAuthenticateService
{
    //public async Task<bool> AuthenticateAsync(string username, string password)
    //{
    //    var restClient = new RestClient("http://auth");
    //    var request = new RestRequest("Login", Method.Post);

    //    // Serialize the UserLogin object to JSON and add it as a header
    //    var userLogin = new UserLogin { Username = username, Password = password };
    //    var json = JsonConvert.SerializeObject(userLogin);
    //    request.AddParameter("UserLogin", json, ParameterType.HttpHeader);

    //    // Send the POST request using RestSharp
    //    var response = await restClient.ExecuteAsync(request);

    //    if (response ) { }
    //    bool isAuthenticated = true;

    //    return isAuthenticated;
    //}

    public async Task<(bool isAuthenticated, string token)> AuthenticateAsync(string username, string password)
    {
        var restClient = new RestClient("http://auth");
        var request = new RestRequest("Login", Method.Post);

        // Serialize the UserLogin object to JSON and add it as a request body
        var userLogin = new UserLogin { Username = username, Password = password };
        var json = JsonConvert.SerializeObject(userLogin);
        request.AddParameter("application/json", json, ParameterType.RequestBody);

        // Send the POST request using RestSharp
        var response = await restClient.ExecuteAsync(request);

        bool isAuthenticated = false;
        string token = string.Empty;

        // Check if the response is okay and contains a token
        if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content.Contains("token"))
        {
            Console.WriteLine(response);
            isAuthenticated = true;
                token = JsonConvert.DeserializeObject<dynamic>(response.Content)["token"];
            }

        return (isAuthenticated, token);
    }

}
}
