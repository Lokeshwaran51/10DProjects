/*using AmazonClone.API.Data.Entity;
using MediatR;

public class SuccessCommand : IRequest<List<AmazonClone.API.Data.Entity.Order>>
{
    public OrderDetail Order { get; set; }

    *//* public class OrderDetail
     {
         public string ProductName { get; set; }
         public decimal Price { get; set; }
         public decimal Total { get; set; }
         public string PaymentMode { get; set; }
         public DateTime OrderDate { get; internal set; }
     }*//*
}*/


using MediatR;

namespace AmazonClone.API.CQRS.Order.Command
{
    public class SuccessCommand : IRequest<List<AmazonClone.API.Data.Entity.Order>>
    {
        public AmazonClone.API.Data.Entity.Order Order { get; set; }
    }
}

