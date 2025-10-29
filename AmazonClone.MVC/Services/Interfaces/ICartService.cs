using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface ICartService
    {
        public Task<HttpResponseMessage> AddToCart(AddToCartDTO dto, string Token);
        public Task<HttpResponseMessage> GetCartItems(string email,string token);
        public Task<HttpResponseMessage> CartItemCount(string Email,string token);
        public Task<HttpResponseMessage> RemoveItemFromCart(int ProductId,string token);
    }
}
