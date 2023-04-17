using System.Collections.Generic;
using LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public interface ILoadBalancerStrategy
    {
        public Service? NextService(List<Service> services);
    }
}
