using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string WorkType { get; set; }
        public string BookAuthors { get; set; }
        public string ChapAuthors { get; set; }
        public string Editors { get; set; }
        public string BookTitle { get; set; }
        public string ChapTitle { get; set; }
        public string FirstPage { get; set; }
        public string LastPage { get; set; }
        public string Editorial { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Isbn { get; set; }
        public string Report { get; set; }


        public struct ColumnNames
        {
            public const string Id = "id";
            public const string WorkType = "worktype";
            public const string BookAuthors = "bookauthors";
            public const string ChapAuthors = "chapauthors";
            public const string Editors = "editors";
            public const string BookTitle = "booktitle";
            public const string ChapTitle = "chaptitle";
            public const string FirstPage = "firstpage";
            public const string LastPage = "lastpage";
            public const string Editorial = "editorial";
            public const string City = "city";
            public const string Country = "country";
            public const string Year = "year";
            public const string Isbn = "isbn";
            public const string Report = "report";
        }
    }
}
