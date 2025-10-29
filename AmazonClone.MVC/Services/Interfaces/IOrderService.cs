namespace AmazonClone.MVC.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<HttpResponseMessage> PlaceOrder(int ProductId, int quantity, sbyte token);
    }
}
