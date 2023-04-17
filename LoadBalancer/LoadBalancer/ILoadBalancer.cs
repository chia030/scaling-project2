using System.Collections.Generic;
using LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public interface ILoadBalancer
    {
        public List<Service> GetAllServices();
        public Guid AddService(string url);
        public Guid RemoveService(Guid id);
        public ILoadBalancerStrategy GetActiveStrategy();
        public void SetActiveStrategy(ILoadBalancerStrategy strategy);
        public Service? NextService();
    }
}