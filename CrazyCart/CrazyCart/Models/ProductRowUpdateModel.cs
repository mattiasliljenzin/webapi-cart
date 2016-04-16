using System;

namespace CrazyCart.Models
{
    public class ProductRowUpdateModel
    {
        public Guid ProductId { get; set; }
        public double Quantity { get; set; } // testet skickar in en double...
    }
}