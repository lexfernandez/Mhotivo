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
        public DbSet<People> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Meister> Meisters { get; set; } 
    }
}