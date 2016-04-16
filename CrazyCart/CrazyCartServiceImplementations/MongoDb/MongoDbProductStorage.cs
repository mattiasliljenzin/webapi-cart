using System;
using System.Collections.Generic;
using System.Configuration;
using CrazyCartDomain.Domain;
using CrazyCartServiceContracts.Contracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace CrazyCartServiceImplementations.MongoDb
{
    public class MongoDbProductStorage : IProductStorage
    {
        private readonly MongoDbConnection<Product> _connection;

        public MongoDbProductStorage()
        {
            _connection =
                new MongoDbConnection<Product>(
                    ConfigurationManager.ConnectionStrings["CrazyCartMongoDbConnectionString"].ConnectionString);
        }

        public MongoDbProductStorage(string mongoConnectionString)
        {
            _connection = new MongoDbConnection<Product>(mongoConnectionString);
        }

        public Product Get(Guid id)
        {
            return _connection.GetById(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _connection.GetAll();
        }

        public void Upsert(Product product)
        {
            if (product.Id == default(Guid))
            {
                product.Id = Guid.NewGuid();
            }
            _connection.Upsert(product);
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
