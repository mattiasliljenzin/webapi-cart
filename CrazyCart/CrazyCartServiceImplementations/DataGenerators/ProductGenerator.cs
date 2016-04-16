using System;
using System.Collections.Generic;
using CrazyCartDomain.Domain;

namespace CrazyCartServiceImplementations.DataGenerators
{
    public class ProductGenerator
    {
        private static readonly Random Random = new Random();

        private readonly IList<string> _colors = new List<string>
        {
            "Green",
            "Blue",
            "Red",
            "White",
            "Black"
        };

        private readonly IList<string> _capacity = new List<string>
        {
            "16GB",
            "32GB",
            "64GB",
            "128GB",
            "256GB"
        };

        private readonly IList<string> _names = new List<string>
        {
            "iPhone 6",
            "iPhone 5S",
            "iPhone 5",
            "iPhone 4S",
            "iPhone 4",
            "iPad 4",
            "iPad Mini Retina",
            "iPad Mini",
            "iPad Air",
            "iPod Classic",
            "iPod Nano",
        };

        public IEnumerable<Product> GenerateProducts(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return GeneratProduct();
            }
        }

        public static void PrintProduct(Product product)
        {
            System.Diagnostics.Debug.WriteLine("Name: " + product.Name);
            System.Diagnostics.Debug.WriteLine("Price: {0}", product.PriceIncVat);
            System.Diagnostics.Debug.WriteLine("VAT: {0}", product.VatAmount);
            System.Diagnostics.Debug.WriteLine("VAT%: {0}", product.VatPercentage);
            System.Diagnostics.Debug.WriteLine(string.Empty);
        }

        private Product GeneratProduct()
        {
            var priceInVat = (Random.NextDouble() + Random.Next(1, 50)) * 100;

            return new Product
            {
                Id = Guid.NewGuid(),
                Name = GenerateProductName(),
                PriceIncVat = Math.Round(priceInVat, 0),
                VatPercentage = Math.Round((decimal)Random.Next(1, 3) / 10, 2)
            };
        }

        private string GenerateProductName()
        {
            return string.Format("{0} {1} {2}",
                _colors[Random.Next(0, _colors.Count - 1)],
                _names[Random.Next(0, _names.Count - 1)],
                _capacity[Random.Next(0, _capacity.Count - 1)]);

        }
    }
}