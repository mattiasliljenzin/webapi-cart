namespace CrazyCart.Models
{
    public class ProductUpdateModel
    {
        public string Name { get; set; }
        public double PriceIncVat { get; set; }
        public decimal VatPercentage { get; set; }
    }
}