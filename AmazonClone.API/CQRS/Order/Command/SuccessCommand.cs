
using AmazonClone.API.Data.DTO;
using MediatR;

namespace AmazonClone.API.CQRS.Order.Command
{
    public class SuccessCommand : IRequest<List<OrderDto>>
    {
        public OrderDto? Order { get; set; }
    }
}

