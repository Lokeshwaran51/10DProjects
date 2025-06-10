using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Order.Command
{
    public class PlaceOrderCommand : IRequest<List<OrderDTO>>
    {
        public OrderDTO Order { get; set; } 
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }

        public PlaceOrderCommand(OrderDTO order, int quantity, int productId, int? userId)
        {
            Order = order;
            Quantity = quantity;
            ProductId = productId;
            UserId = userId;
        }
    }
}
