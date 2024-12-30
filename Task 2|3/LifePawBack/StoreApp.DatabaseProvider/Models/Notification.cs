using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DatabaseProvider.Models
{
    public class Notification
    {
        public int Id { get; set; }  // Primary Key
           
        public string Type { get; set; }         
        public string Message { get; set; }       
        public DateTime DateTime { get; set; }
        public User User { get; set; }     
        public int UserId { get; set; }         
        public Animal Animal { get; set; }
        public int AnimalId { get; set; }
    }
}
