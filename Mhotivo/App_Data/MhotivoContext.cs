using System.Data.Entity;
using Mhotivo.Models;

namespace Mhotivo.App_Data
{
    public class MhotivoContext : DbContext
    {
        public MhotivoContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<People> Peoples { get; set; }
    }
}