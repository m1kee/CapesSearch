using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capes.API.Models
{
    public class BookFilters
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("dateRange")]
        public DateRange DateRange { get; set; }
    }

    public class DateRange
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}