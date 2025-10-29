using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Order.Command
{
    public class PlaceOrderCommand : IRequest<List<OrderDto>>
    {
        public OrderDto Order { get; set; } 
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }

        public PlaceOrderCommand(OrderDto order, int quantity, int productId, int? userId)
        {
            Order = order;
            Quantity = quantity;
            ProductId = productId;
            UserId = userId;
        }
    }
}
