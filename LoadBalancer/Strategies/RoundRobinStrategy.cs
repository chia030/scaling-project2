using LoadBalancer.Models;
using LoadBalancer.LoadBalancer;

namespace LoadBalancer.Strategies
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        private int _next;

        public Service? NextService(List<Service> services)
        {
            lock (this)
            {
                if (services.Count == 0) return null;
                if (_next >= services.Count) _next = 0;

                var service = services[_next];
                _next++;
                return service;
            }
        }
    }
}