using MediatR;
using OrderServices.Data.DTOs;

namespace OrderServices.Order.Command
{
    public class SuccessCommand : IRequest<List<OrderDto>>
    {
        public OrderDto? Order { get; set; }
    }
}
