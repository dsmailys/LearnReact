using System.Collections.Generic;
using ImdbDataRefresher.Models;
using LiteDB;

namespace imdbApi.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : DataModelBase
    {
        private readonly LiteCollection<T> collection;

        public RepositoryBase(LiteDatabase db) {
            collection = db.GetCollection <T> ();
        }

        public T Create(T item)
        {
            collection.Insert (new BsonValue (item.Id), item);
            return item;
        }

        public bool Delete(string id)
        {
            return collection.Delete (new BsonValue (id));
        }

        public T Get(string id)
        {
            return collection.FindOne ((item) => item.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return collection.FindAll ();
        }

        public IEnumerable<T> GetPage(int start, int count)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(string id, T item)
        {
            return collection.Update (new BsonValue (id), item);
        }
    }
}