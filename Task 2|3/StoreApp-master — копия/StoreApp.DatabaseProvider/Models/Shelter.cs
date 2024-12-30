using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DatabaseProvider.Models
{
    public class Shelter
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contacts { get; set; }
    }
}
