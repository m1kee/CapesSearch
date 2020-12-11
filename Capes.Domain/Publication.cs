using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Domain
{
    public class Publication
    {
        public int Id { get; set; }
        public string Doi { get; set; }
        public string Authors { get; set; }
        public string ArticleTitle { get; set; }
        public string JournalName { get; set; }
        public string Volume { get; set; }
        public string Year { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string Notes { get; set; }
        public string Report { get; set; }

        public struct ColumnNames
        {
            public const string Id = "n";
            public const string Doi = "doi";
            public const string Authors = "authors";
            public const string ArticleTitle = "articletitle";
            public const string JournalName = "journalname";
            public const string Volume = "volume";
            public const string Year = "year";
            public const string FirstPage = "firstpage";
            public const string LastPage = "lastpage";
            public const string Notes = "notes";
            public const string Report = "report";
        }
    }
}
