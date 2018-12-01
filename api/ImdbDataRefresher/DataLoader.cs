using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ImdbDataRefresher.Models;
using LiteDB;

namespace ImdbDataRefresher
{
    public class DataLoader : IDataRefresher
    {
        public void LoadImdbBasicsData()
        {
            using(var db = new LiteDatabase(@"MyData.db"))
            {
                var col = db.GetCollection<Movie>("Movie");
                var movies = col.FindAll ();
                var count = movies.Count();

                if (count == 0)
                    loadData ();
                    Console.WriteLine("Test");
            }
        }

        private void loadData()
        {
            var data = new List<Movie> ();
            using (var fileStream = new FileStream(@"D:\Downloads\title.basics.tsv\data.tsv", System.IO.FileMode.Open))
                using (var reader = new StreamReader (fileStream)) {
                    var line = reader.ReadLine (); // Skipping header line
                    if (line == null)
                        throw new Exception ("File is empty and without a header.");

                    line = reader.ReadLine ();
                    while (line != null) {
                        var lineParts = line.Split('\t');
                        if (lineParts.Count () != 9)
                            throw new FormatException ("Unfamiliar format was found");

                        var movie = new Movie {
                            Id = lineParts[0].Trim(),
                            TitleType = lineParts[1].Trim(),
                            PrimaryTitle = lineParts[2].Trim(),
                            OriginalTitle = lineParts[3].Trim(),
                            IsAdult = ParseToBoolean (lineParts[4].Trim()),
                            StartYear = ParseToInt (lineParts[5].Trim()),
                            EndYear = ParseToInt (lineParts[6].Trim()),
                            RuntimeInMinutes = ParseToInt (lineParts[7].Trim()),
                            Genres = ParseGenres(lineParts[8].Trim())
                        };

                        data.Add (movie);
                        line = reader.ReadLine ();
                    }
                }
                var a =0;
        }

        private int ParseToInt(string value)
        {
            if (Int32.TryParse (value, out var parsed))
                return parsed;
            return 0;
        }

        private IList<string> ParseGenres(string value)
        {
            var genreList = new List<string>();
            var genres = value.Split (',');

            foreach (var genre in genres) {
                genreList.Add (genre.Trim());
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
