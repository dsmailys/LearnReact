using ImdbDataRefresher.Models;
using LiteDB;

namespace imdbApi.Repositories
{
    public class MovieRepository : RepositoryBase<Movie>
    {
        public MovieRepository(LiteDatabase db) : base(db)
        {
        }
    }
}