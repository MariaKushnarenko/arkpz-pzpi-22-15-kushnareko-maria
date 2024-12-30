using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider.Models;

namespace StoreApp.DatabaseProvider
{
    public class DataContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Shelter> Shelter { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Sensor> Sensor { get; set; }
        public DbSet<User> User { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
