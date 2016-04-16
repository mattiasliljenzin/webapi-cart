using System;

namespace CrazyCart.Models
{
    public class ProductRowModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PriceIncVat { get; set; }
        public decimal VatPercentage { get; set; }

        public double PriceIncVatAmount { get { return PriceIncVat * Quantity; } }
        public double VatAmount { get { return PriceIncVat * (double)VatPercentage; } }

        public int Quantity { get; set; }
    }
}