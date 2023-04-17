using LoadBalancer.LoadBalancer;
using LoadBalancer.Models;

namespace LoadBalancer.Strategies
{
    public class RandomStrategy : ILoadBalancerStrategy
    {
        public Service? NextService(List<Service> services)
        {
            if (services.Count == 0) return null;

            var random = new Random(Guid.NewGuid().GetHashCode());
            return services[random.Next(services.Count)];
        }
    }
}
