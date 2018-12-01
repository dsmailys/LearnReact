using System.Collections.Generic;
using ImdbDataRefresher.Models;

namespace imdbApi.Repositories
{
    public interface IRepository<T> where T : DataModelBase
    {
        IEnumerable<T> GetAll ();
        IEnumerable<T> GetPage (int start, int count);
        T Get (string id);
        bool Delete (string id);
        bool Update (string id, T item);
        T Create (T item);
    }
}