﻿namespace AmazonClone.API.Data.DTO
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        //public decimal Total => Quantity * Price;
    }

}
