
using LoadBalancer.Strategies;
using Microsoft.AspNetCore.Mvc;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("configuration")]
    public class ConfigurationController : ControllerBase
    {
        public ConfigurationController()
        {
            LoadBalancer.LoadBalancer.GetInstance()
                .SetActiveStrategy(new RandomStrategy());
        }

        [HttpPost]
        public Guid Post([FromQuery] string? url)
        {
            Console.WriteLine("LB: Adding service at URL " + url);
            return LoadBalancer.LoadBalancer.GetInstance().AddService(url);
        }
    }
}
