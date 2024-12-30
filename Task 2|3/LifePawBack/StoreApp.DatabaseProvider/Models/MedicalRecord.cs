using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DatabaseProvider.Models
{
    public class MedicalRecords
    {
        public int Id { get; set; }   // Primary Key
        public Animal Animal { get; set; }
        public int AnimalId { get; set; } 
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
