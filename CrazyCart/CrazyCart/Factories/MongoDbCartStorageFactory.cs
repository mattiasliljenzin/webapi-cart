using CrazyCartServiceContracts.Contracts;
using CrazyCartServiceImplementations.MongoDb;

namespace CrazyCart.Factories
{
    public class MongoDbCartStorageFactory : ICartStorageFactory
    {
        public ICartStorage Build()
        {
            return new MongoDbCartStorage();
        }

        public ICartStorage Build(string connectionString)
        {
            return new MongoDbCartStorage(connectionString);
        }
    }
}