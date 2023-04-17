namespace LoadBalancer.Models
{
    public class Service
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Url { get; }

        public Service(string url)
        {
            Url = url;
        }
    }
}