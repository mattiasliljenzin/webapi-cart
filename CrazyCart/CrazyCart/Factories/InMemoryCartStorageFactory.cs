using System;
using System.Linq;
using CrazyCartServiceContracts.Contracts;
using CrazyCartServiceImplementations.DataGenerators;
using CrazyCartServiceImplementations.InMemory;

namespace CrazyCart.Factories
{
    public class InMemoryCartStorageFactory : ICartStorageFactory
    {
        public ICartStorage Build()
        {
            var random = new Random();
            var storage = new InMemoryCartStorage();
            var cartGenerator = new CartGenerator();
            var generatedCarts = cartGenerator.GenerateCarts(10).ToList();


            generatedCarts.ForEach(x =>
            {
                cartGenerator.PopulateCartWithProducts(x, random.Next(1, 10));
                storage.Upsert(x);
            });

            return storage;
        }

        public ICartStorage Build(string connectionString)
        {
            return Build();
        }
    }
}