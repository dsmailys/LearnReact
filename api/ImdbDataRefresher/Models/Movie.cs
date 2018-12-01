using System.Collections.Generic;

namespace ImdbDataRefresher.Models
{
    public class Movie : DataModelBase
    {
        public override string Id { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int RuntimeInMinutes { get; set; }
        public IList<string> Genres { get; set; }
    }
}