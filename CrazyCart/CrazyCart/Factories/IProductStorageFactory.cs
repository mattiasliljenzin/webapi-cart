using CrazyCartServiceContracts.Contracts;

namespace CrazyCart.Factories
{
    public interface IProductStorageFactory 
    {
        IProductStorage Build();
        IProductStorage Build(string connectionString);
    }
}