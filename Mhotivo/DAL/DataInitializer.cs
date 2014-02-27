using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Mhotivo.Models;

namespace Mhotivo.DAL
{
    public class DataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<Mhotivo.App_Data.MhotivoContext>
    {
        protected override void Seed(Mhotivo.App_Data.MhotivoContext context)
        {
            var Role = new List<Role>
            {
                new Role{Name="All mighty", Description="It is chuck norris",}
            };
            Role.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            var User = new List<User>
            {
                new User{Email="gmail@chucknorris.com", Password="1234567890", DisplayName="Chuck Norris", Status=true, Role = Role[0]}
            };
            User.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var People = new List<People>
            {
                new Student{FirstName="Miguel Alejandro", LastName="Solis Madrid", FullName="Miguel Alejandro Solis Madrid", DateOfBirth=DateTime.Parse("1992-01-14"), Gender='M', IDNumber="0501-1992-03345", Nationality="Hondureña", City="San Pedro Sula", State="Cortes", StreetAddress="Col. Jardines del Valle", BloodType="O+", StartDate=DateTime.Parse("2014-02-02"), AccountNumber="20941145", Biography="Bueno"},
                new Parent{FirstName="Chuck", LastName="Norris", FullName="Chuck Norris", DateOfBirth=DateTime.Parse("1940-03-10"), Gender='M', IDNumber="0501-1940-32541", Nationality="Hondureña", City="San Pedro Sula", State="Cortes", StreetAddress="Col. Jardines del Valle"}
            };
            People.ForEach(s => context.People.Add(s));
            context.SaveChanges();
        }
    }
}