using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Domain
{
    public class Participation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string NameEvent { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Modality { get; set; }
        public string Title { get; set; }
        public string CapesPersons { get; set; }
        public string Report { get; set; }

        public struct ColumnNames
        {
            public const string Id = "id";
            public const string Type = "type";
            public const string NameEvent = "namevent";
            public const string Country = "country";
            public const string City = "city";
            public const string Modality = "modality";
            public const string Title = "title";
            public const string CapesPersons = "capespersons";
            public const string Report = "report";
        }
    }
}
