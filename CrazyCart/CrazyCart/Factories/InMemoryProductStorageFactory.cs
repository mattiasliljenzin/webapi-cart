using System.Linq;
using CrazyCartServiceContracts.Contracts;
using CrazyCartServiceImplementations.DataGenerators;
using CrazyCartServiceImplementations.InMemory;

namespace CrazyCart.Factories
{
    public class InMemoryProductStorageFactory : IProductStorageFactory
    {
        public IProductStorage Build()
        {
            var storage = new InMemoryProductStorage();
            var productGenerator = new ProductGenerator();
            var generatedProducts = productGenerator.GenerateProducts(10).ToList();

            generatedProducts.ForEach(storage.Upsert);

            return storage;
        }

        public IProductStorage Build(string connectionString)
        {
            return Build();
        }
    }
}