namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IProductService
    {
        public Task<HttpResponseMessage> GetListOfProductsBySubCategoryId(int SubCategoryId,string token);
        public Task<HttpResponseMessage> ProductDetails(int Id, string token);
    }
}
