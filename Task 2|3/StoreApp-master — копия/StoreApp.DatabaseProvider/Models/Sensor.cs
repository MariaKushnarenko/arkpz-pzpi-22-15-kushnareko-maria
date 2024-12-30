using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DatabaseProvider.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime InstallationDate { get; set; }

        public Animal? Animal { get; set; }
        public int AnimalId { get; set; } 
    }
}
