using CrazyCartServiceContracts.Contracts;
using CrazyCartServiceImplementations.MongoDb;

namespace CrazyCart.Factories
{
    public class MongoDbProductStorageFactory : IProductStorageFactory
    {
        public IProductStorage Build()
        {
            return new MongoDbProductStorage();
        }

        public IProductStorage Build(string connectionString)
        {
            return new MongoDbProductStorage(connectionString);
        }
    }
}