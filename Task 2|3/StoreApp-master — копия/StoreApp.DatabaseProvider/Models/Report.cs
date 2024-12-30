using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DatabaseProvider.Models
{
    public class Report
    {
        public int Id { get; set; }        // Primary Key
        public string Report_Type { get; set; }
        public DateTime Date { get; set; }
        public string CreationPeriod { get; set; }
        public string Description { get; set; }
        public string File_path { get; set; }

        public Shelter Shelter { get; set; }
        public int ShelterId { get; set; }
    }
}
