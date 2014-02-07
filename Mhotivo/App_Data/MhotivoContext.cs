using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Mhotivo.Models;

namespace Mhotivo.App_Data
{
    public class MhotivoContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}