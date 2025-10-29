using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace AmazonClone.MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<HttpResponseMessage> GetAllCategories(string token);
        public Task<HttpResponseMessage> GetSubCategoryByCategoryId(int categoryId,string token);
    }
}
