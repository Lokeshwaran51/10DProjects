using AmazonClone.MVC.Models;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IUserService
    {
        public Task<HttpResponseMessage> Register(User user, string Token);
        public Task<HttpResponseMessage> Login(Login login);
    }
}
