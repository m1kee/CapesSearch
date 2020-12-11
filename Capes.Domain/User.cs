using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capes.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public struct ColumnNames
        {
            public const string Id = "n";
            public const string Email = "email";
            public const string Password = "pass";
        }
    }
}
