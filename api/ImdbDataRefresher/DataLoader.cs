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
            }
        }

        private void loadData()
        {
            var data = new List<Movie> ();
            using (var fileStream = new FileStream(@"D:\Downloads\title.basics.tsv\data.tsv", System.IO.FileMode.Open))
                using (var reader = new StreamReader (fileStream)) {
                    var line = reader.ReadLine ();
                    while (line != null) {
                        line = reader.ReadLine ();

                        var lineParts = line.Split('\t');
                        if (lineParts.Count () != 6)
                            throw new FormatException ("Unfamiliar format was found");

                        var movie = new Movie {
                            Id = lineParts[0]

                        }
                    }
                }
        }
    }
}
