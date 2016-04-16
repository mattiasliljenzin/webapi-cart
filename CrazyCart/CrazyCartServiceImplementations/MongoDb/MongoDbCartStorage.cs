using System;
using System.Collections.Generic;
using System.Configuration;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace CrazyCartServiceImplementations.MongoDb
{
    public class MongoDbCartStorage : ICartStorage
    {
        private readonly MongoDbConnection<Cart> _connection;

        public MongoDbCartStorage()
        {
            _connection =
                new MongoDbConnection<Cart>(
                    ConfigurationManager.ConnectionStrings["CrazyCartMongoDbConnectionString"].ConnectionString);
        }

        public MongoDbCartStorage(string mongoConnectionString)
        {
            _connection = new MongoDbConnection<Cart>(mongoConnectionString);
        }

        public Cart Get(Guid id)
        {
            return _connection.GetById(id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return _connection.GetAll();
        }

        public void Upsert(Cart cart)
        {
            GenerateIdForNewCart(cart);
            GenerateIdForNewProductRows(cart);

            _connection.Upsert(cart);
        }

        private static void GenerateIdForNewProductRows(Cart cart)
        {
            foreach (var productRow in cart.Rows)
            {
                if (productRow.Product.Id == default(Guid))
                {
                    productRow.Product.Id = Guid.NewGuid();
                }
            }
        }

        private static void GenerateIdForNewCart(Cart cart)
        {
            if (cart.Id == default(Guid))
            {
                cart.Id = Guid.NewGuid();
            }
        }

        public void Remove(Guid id)
        {
            _connection.Remove(new FindAndRemoveArgs
            {
                Query = Query.EQ("_id", id)
            });
        }

        public long Count()
        {
            return _connection.Count();
        }
    }
}