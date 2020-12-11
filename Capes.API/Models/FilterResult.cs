using Capes.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capes.API.Models
{
    public class FilterResult
    {
        [JsonProperty("books")]
        public List<Book> Books { get; set; }

        [JsonProperty("awards")]
        public List<Award> Awards { get; set; }

        [JsonProperty("participations")]
        public List<Participation> Participations { get; set; }

        [JsonProperty("publications")]
        public List<Publication> Publications { get; set; }
    }
}