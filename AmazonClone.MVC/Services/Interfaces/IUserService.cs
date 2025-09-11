using AmazonClone.MVC.Models;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<HttpResponseMessage> Register(User user, string Token);
        Task<HttpResponseMessage> Login(Login login);
    }
}
