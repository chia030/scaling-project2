public interface IAuthenticateService
{
    Task<(bool isAuthenticated, string token)> AuthenticateAsync(string username, string password);
}
