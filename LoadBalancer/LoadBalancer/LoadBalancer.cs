using LoadBalancer.LoadBalancer;
using System.Collections.Generic;
using LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public class LoadBalancer : ILoadBalancer
    {
        private ILoadBalancerStrategy? _strategy;
        private readonly Dictionary<Guid, Service> _services = new();
        public static LoadBalancer? _instance;

        private LoadBalancer()
        { }

        public static LoadBalancer GetInstance()
        {
            return _instance ??= new LoadBalancer();
        }

        public List<Service> GetAllServices()
        {
            return _services.Values.ToList();
        }

        public Guid AddService(string url)
        {
            var service = new Service(url);
            _services.Add(service.Id, service);
            return service.Id;
        }

        public Guid RemoveService(Guid id)
        {
            _services.Remove(id);
            return id;
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            return _strategy;
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            _strategy = strategy;
        }

        public Service? NextService()
        {
            return _strategy?.NextService(GetAllServices());
        }
    }
}