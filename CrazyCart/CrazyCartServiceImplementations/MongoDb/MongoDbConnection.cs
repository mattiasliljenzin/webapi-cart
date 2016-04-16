using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CrazyCartServiceImplementations.MongoDb
{
    public class MongoDbConnection<T> where T : class
    {
        private readonly MongoCollection<T> _collection;

        public MongoDbConnection(string mongoConnectionString)
        {
            var builder = new MongoUrlBuilder(mongoConnectionString);
            var client = new MongoClient(mongoConnectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(builder.DatabaseName);
            
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public T GetById(BsonValue id)
        {
            return _collection.FindOneByIdAs<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.FindAllAs<T>();
        }

        public void Upsert(T t)
        {
            _collection.Save(t);
        }

        public void Remove(FindAndRemoveArgs args)
        {
            _collection.FindAndRemove(args);
        }

        public long Count()
        {
            return _collection.Count();
        }
    }
}