using AmazonClone.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface ICartService
    {
        Task<HttpResponseMessage> AddToCart(AddToCartDTO dto, string Token);
        Task<HttpResponseMessage> GetCartItems(string email,string token);
        Task<HttpResponseMessage> CartItemCount(string Email,string token);
        Task<HttpResponseMessage> RemoveItemFromCart(int ProductId,string token);
    }
}
