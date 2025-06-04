using MediatR;

namespace AmazonClone.API.Features.Order.Command
{
    public class PlaceOrderCommand : IRequest<List<AmazonClone.API.Data.Entity.Order>>
    {
        public OrderDetail Order { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }

        public class OrderDetail
        {
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}
