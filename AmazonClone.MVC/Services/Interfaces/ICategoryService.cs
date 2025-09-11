using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<HttpResponseMessage> GetAllCategories(string token);
        Task<HttpResponseMessage> GetSubCategoryByCategoryId(int categoryId,string token);
    }
}
