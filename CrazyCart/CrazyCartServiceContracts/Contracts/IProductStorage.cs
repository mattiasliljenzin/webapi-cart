using System;
using System.Collections.Generic;
using CrazyCartDomain.Domain;

namespace CrazyCartServiceContracts.Contracts
{
    public interface IProductStorage : IStorage
    {
        Product Get(Guid id);
        IEnumerable<Product> GetAll();
        void Upsert(Product product);
        void Remove(Guid id);
        long Count();
    }
}