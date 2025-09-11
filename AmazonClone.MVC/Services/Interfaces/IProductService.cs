namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<HttpResponseMessage> GetListOfProductsBySubCategoryId(int SubCategoryId,string token);
        Task<HttpResponseMessage> ProductDetails(int Id, string token);
    }
}
