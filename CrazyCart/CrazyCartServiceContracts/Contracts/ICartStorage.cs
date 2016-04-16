using System;
using System.Collections.Generic;
using CrazyCartDomain.Domain;

namespace CrazyCartServiceContracts.Contracts
{
    public interface ICartStorage : IStorage
    {
        Cart Get(Guid id);
        IEnumerable<Cart> GetAll();
        void Upsert(Cart cart);
        void Remove(Guid id);
        long Count();
    }
}