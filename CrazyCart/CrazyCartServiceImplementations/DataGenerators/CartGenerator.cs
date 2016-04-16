using System;
using System.Collections.Generic;
using System.Linq;
using CrazyCartDomain.Domain;

namespace CrazyCartServiceImplementations.DataGenerators
{
    public class CartGenerator
    {
        public IEnumerable<Cart> GenerateCarts(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new Cart
                {
                    Id = Guid.NewGuid()
                };
            }
        }

        public void PopulateCartWithProducts(Cart cart, int count)
        {
            var random = new Random();
            var productGenerator = new ProductGenerator();

            var generatedProducts = productGenerator.GenerateProducts(count).ToList();
            generatedProducts.ForEach(x => cart.Rows.Add(new ProductRow
            {
                Product = x,
                Quantity = random.Next(1, 5)
            }));
        }
    }
}