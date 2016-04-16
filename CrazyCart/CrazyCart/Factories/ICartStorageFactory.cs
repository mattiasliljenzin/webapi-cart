using CrazyCartServiceContracts.Contracts;

namespace CrazyCart.Factories
{
    public interface ICartStorageFactory
    {
        ICartStorage Build();
        ICartStorage Build(string connectionString);
    }
}