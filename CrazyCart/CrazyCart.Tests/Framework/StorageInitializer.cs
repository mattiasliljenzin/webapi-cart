using System;
using System.Linq;
using System.Reflection;
using CrazyCartServiceContracts.Contracts;
using CrazyCartServiceImplementations.DataGenerators;
using CrazyCartServiceImplementations.InMemory;
using NUnit.Framework;

namespace CrazyCart.Tests.Framework
{
    [TestFixture, Ignore("Run manually to populate storage data")]
    public class StorageInitializer
    {
        [Test, Ignore]
        public void Populate_products()
        {
            // Arrange
            var generatedProducts = new ProductGenerator().GenerateProducts(10).ToList();

            var productImplementations = Assembly.GetAssembly(typeof (InMemoryProductStorage))
                .GetTypes()
                .Where(x => typeof (IProductStorage).IsAssignableFrom(x))
                .ToList();

            // Act
            productImplementations.ForEach(x =>
            {
                System.Diagnostics.Debug.WriteLine("Trying to create: " + x.Name);

                var productStorage = (IProductStorage) Activator.CreateInstance(x);
                generatedProducts.ForEach(productStorage.Upsert);

                System.Diagnostics.Debug.WriteLine("{0} was populated with {1} products", productStorage.GetType().Name, productStorage.Count());
                System.Diagnostics.Debug.WriteLine(string.Empty);
            });
        }

        [Test, Ignore]
        public void Populate_carts_with_products()
        {
            // Arrange
            var random = new Random();
            var cartGenerator = new CartGenerator();
            var generatedCarts = cartGenerator.GenerateCarts(5).ToList();

            foreach (var generatedCart in generatedCarts)
            {
                cartGenerator.PopulateCartWithProducts(generatedCart, random.Next(1, 5));
            }

            var cartImplementations = Assembly.GetAssembly(typeof(InMemoryCartStorage))
                .GetTypes()
                .Where(x => typeof(ICartStorage).IsAssignableFrom(x))
                .ToList();

            // Act
            cartImplementations.ForEach(x =>
            {
                System.Diagnostics.Debug.WriteLine("Trying to create: " + x.Name);

                var cartStorage = (ICartStorage)Activator.CreateInstance(x);
                generatedCarts.ForEach(cartStorage.Upsert);

                System.Diagnostics.Debug.WriteLine("{0} was populated with {1} carts", cartStorage.GetType().Name, cartStorage.Count());
                System.Diagnostics.Debug.WriteLine(string.Empty);
            });
        }
    }
}