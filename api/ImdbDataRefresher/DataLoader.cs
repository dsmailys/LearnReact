using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ImdbDataRefresher.Models;
using LiteDB;

namespace ImdbDataRefresher
{
    internal class DataLoader : IDataRefresher
    {
        public void LoadImdbBasicsData()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Movie> ();
                col.EnsureIndex(movie => movie.Id, true);

                var movies = col.FindAll();
                var count = movies.Count();

                if (count == 0)
                {
                    loadAndSaveTitlesBasicData(col);
                }

            }
        }

        private void loadAndSaveTitlesBasicData(LiteCollection<Movie> col)
        {
            var movieBatch = new List<Movie> (50);
            var count = 0;
            using (var fileStream = new FileStream(@"/home/deividas/Downloads/title.basics.tsv", System.IO.FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                var line = reader.ReadLine(); // Skipping header line
                if (line == null)
                    throw new Exception("File is empty and without a header.");

                line = reader.ReadLine();
                while (line != null)
                {
                    if (movieBatch.Count >= movieBatch.Capacity) {
                        col.Insert (movieBatch);
                        movieBatch.Clear ();
                    }

                    var lineParts = line.Split('\t');
                    if (lineParts.Count() != 9)
                        throw new FormatException("Unfamiliar format was found");

                    var movie = new Movie
                    {
                        Id = lineParts[0].Trim(),
                        TitleType = lineParts[1].Trim(),
                        PrimaryTitle = lineParts[2].Trim(),
                        OriginalTitle = lineParts[3].Trim(),
                        IsAdult = ParseToBoolean(lineParts[4].Trim()),
                        StartYear = ParseToInt(lineParts[5].Trim()),
                        EndYear = ParseToInt(lineParts[6].Trim()),
                        RuntimeInMinutes = ParseToInt(lineParts[7].Trim()),
                        Genres = ParseGenres(lineParts[8].Trim())
                    };

                    movieBatch.Add (movie);
                    count++;
                    line = reader.ReadLine();

                    if (count > 103)
                        break;
                }

                if (movieBatch.Any ()) {
                    col.Insert (movieBatch);
                    movieBatch.Clear ();
                }
            }

            Console.WriteLine ("Data count is:" + count);
        }

        private int ParseToInt(string value)
        {
            if (Int32.TryParse(value, out var parsed))
                return parsed;
            return 0;
        }

        private IList<string> ParseGenres(string value)
        {
            var genreList = new List<string>();
            var genres = value.Split(',');

            foreach (var genre in genres)
            {
                genreList.Add(genre.Trim());
            }

            return genreList;
        }

        private bool ParseToBoolean(string value)
        {
            if (value == "0")
                return false;
            return true;
        }
    }
}
