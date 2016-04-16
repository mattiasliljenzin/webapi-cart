using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;

namespace CrazyCartServiceImplementations.InMemory
{
    public class InMemoryProductStorage : IProductStorage
    {
        private static readonly IDictionary<Guid, Product> Storage = new ConcurrentDictionary<Guid, Product>();

        public Product Get(Guid id)
        {
            return Storage.ContainsKey(id) ? Storage[id] : null;
        }

        public IEnumerable<Product> GetAll()
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

        public void Upsert(Product product)
        {
            if (product.Id == default(Guid))
            {
                product.Id = Guid.NewGuid();
            }

            if (Storage.ContainsKey(product.Id))
            {
                Storage[product.Id] = product;
            }
            else
            {
                Storage.Add(product.Id, product);
            }

        }
    }
}
