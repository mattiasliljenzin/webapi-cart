using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;

namespace CrazyCartServiceImplementations.InMemory
{
    public class InMemoryCartStorage : ICartStorage
    {
        private static readonly IDictionary<Guid, Cart> Storage = new ConcurrentDictionary<Guid, Cart>();

        public Cart Get(Guid id)
        {
            return Storage.ContainsKey(id) ? Storage[id] : null;
        }

        public IEnumerable<Cart> GetAll()
        {
            return Storage.Select(x => x.Value);
        }

        public void Remove(Guid id)
        {
            Storage.Remove(id);
        }

        public long Count()
        {
            return Storage.Count;
        }

        public void Upsert(Cart cart)
        {
            if (cart.Id == default(Guid))
            {
                cart.Id = Guid.NewGuid();
            }

            if (Storage.ContainsKey(cart.Id))
            {
                Storage[cart.Id] = cart;
            }
            else
            {
                Storage.Add(cart.Id, cart);
            }
        }
    }
}