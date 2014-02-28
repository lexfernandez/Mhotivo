using System.Data.Entity;
using Mhotivo.Models;

namespace Mhotivo.App_Data
{
    public class MhotivoContext : DbContext
    {
        public MhotivoContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<AppointmentDiary> AppointmentDiary { get; set; }
    }
}