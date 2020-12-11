using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Domain
{
    public class Award
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Awardee { get; set; }
        public string Contribution { get; set; }
        public string Institution { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Report { get; set; }

        public struct ColumnNames
        {
            public const string Id = "id";
            public const string Name = "award";
            public const string Awardee = "awardee";
            public const string Contribution = "contribution";
            public const string Institution = "institution";
            public const string Country = "country";
            public const string Year = "year";
            public const string Report = "report";
        }
    }
}
