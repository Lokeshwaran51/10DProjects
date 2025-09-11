namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IOrderService
    {
        Task<HttpResponseMessage> PlaceOrder(int ProductId, int quantity, sbyte token);
    }
}
