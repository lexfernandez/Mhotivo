using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Mhotivo.Models;

namespace Mhotivo.App_Data
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MhotivoContext>
    {
        protected override void Seed(MhotivoContext context)
        {
            var role = new List<Role>
            {
            new Role{Name="Student",Description="Role of the student."},
            new Role{Name="Teacher",Description="Role of the teacher."},
            new Role{Name="Parent",Description="Role of the parent."},
            new Role{Name="Principal",Description="Role of the principal."},
            };
            role.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            var user = new List<User>
            {
            new User{Email="someone@mhotivo.hn",DisplayName="Alexander",Password="pokemon1", Status=true,},
            new User{Email="someone.else@mhotivo.hn",DisplayName="Pedro",Password="pokemon2", Status=true, },
            new User{Email="another.dude@mhotivo.hn",DisplayName="Pablo",Password="pokemon3", Status=true, },
            new User{Email="soyfresa@mhotivo.hn",DisplayName="Fresa",Password="pokemon4", Status=true, },
            };
            user.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}