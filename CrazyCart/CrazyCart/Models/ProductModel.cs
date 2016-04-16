using System;

namespace CrazyCart.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PriceIncVat { get; set; }
        public decimal VatPercentage { get; set; }

        public double VatAmount { get { return PriceIncVat * (double)VatPercentage; } }
    }
}