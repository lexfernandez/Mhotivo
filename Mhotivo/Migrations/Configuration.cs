using System.Collections.ObjectModel;
using Mhotivo.Models;

namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mhotivo.App_Data.MhotivoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Mhotivo.App_Data.MhotivoContext";
        }

        protected override void Seed(Mhotivo.App_Data.MhotivoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(new Role{Id = 1,Description = "Admin", Name = "Admin"});
            context.SaveChanges();
            context.Users.AddOrUpdate(new User {Id = 1,DisplayName = "Alex Fernandez", Email = "olorenzo@outlook.com", Password = "123", Role = context.Roles.First(), Status = true });
            context.Users.AddOrUpdate(new User { Id = 2, DisplayName = "Franklin Castellanos", Email = "castellarfrank@hotmail.com", Password = "siniestro", Role = context.Roles.First(), Status = true });
            context.Users.AddOrUpdate(new User { Id = 3, DisplayName = "Alejandro Solis", Email = "solis1492@gmail.com", Password = "asdf1492", Role = context.Roles.First(), Status = true });

            #region Atlantida
            context.States.AddOrUpdate(new State {Id = 1, Name = "Atlantida" });
            context.Cities.AddOrUpdate(new City { Id = 1, Name = "La Ceiba", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 2, Name = "El Porvenir", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 3, Name = "Tela", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 4, Name = "Jutiapa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 5, Name = "La Masica", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 6, Name = "San Francisco", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 7, Name = "Arizona", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 8, Name = "Esparta", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 9, Name = "Trujillo", State = context.States.Last() });
            #endregion

            #region Colon
            context.States.AddOrUpdate(new State { Id = 2, Name = "Colón" });
            context.Cities.AddOrUpdate(new City { Id = 10, Name = "Balfate", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 11, Name = "Iriona", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 12, Name = "Limón", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 13, Name = "Sabá", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 14, Name = "Santa Fe", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 15, Name = "Santa Rosa de Aguán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 16, Name = "Sonaguera", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 17, Name = "Tocoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 18, Name = "Bonito Oriental", State = context.States.Last() });
            #endregion

            #region Comayagua
            context.States.AddOrUpdate(new State { Id = 3, Name = "Comayagua" });
            context.Cities.AddOrUpdate(new City { Id = 19, Name = "Comayagua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 20, Name = "Ajuterique", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 21, Name = "El Rosario", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 22, Name = "Esquías", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 23, Name = "Humuya", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 24, Name = "La libertad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 25, Name = "Lamaní", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 26, Name = "La Trinidad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 27, Name = "Lejamani", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 28, Name = "Meambar", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 29, Name = "Minas de Oro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 30, Name = "Ojos de Agua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 31, Name = "San Jerónimo", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 32, Name = "San José de Comayagua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 33, Name = "San José del Potrero", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 34, Name = "San Luis", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 35, Name = "San Sebastián", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 36, Name = "Siguatepeque", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 37, Name = "Villa de San Antonio", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 38, Name = "Las Lajas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 39, Name = "Taulabé", State = context.States.Last() });
            #endregion

            #region Copan
            context.States.AddOrUpdate(new State { Id = 4, Name = "Copán" });
            context.Cities.AddOrUpdate(new City { Id = 40, Name = "Santa Rosa de Copán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 41, Name = "Cabañas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 42, Name = "Concepción", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 43, Name = "Copán Ruinas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 44, Name = "Corquín", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 45, Name = "Cucuyagua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 46, Name = "Dolores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 47, Name = "Dulce Nombre", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 48, Name = "El Paraíso", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 49, Name = "Florida", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 50, Name = "La Jigua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 51, Name = "La Unión", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 52, Name = "Nueva Arcadia", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 53, Name = "San Agustín", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 54, Name = "San Antonio", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 55, Name = "San Jerónimo", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 56, Name = "San José", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 57, Name = "San Juan de Opoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 58, Name = "San Nicolás", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 59, Name = "San Pedro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 60, Name = "Santa Rita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 61, Name = "Trinidad de Copán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 62, Name = "Veracruz", State = context.States.Last() });
            #endregion

            #region Cortes
            context.States.AddOrUpdate(new State { Id = 5, Name = "Cortés" });
            context.Cities.AddOrUpdate(new City { Id = 63, Name = "San Pedro Sula", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 64, Name = "Choloma", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 65, Name = "Omoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 66, Name = "Pimienta", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 67, Name = "Potrerillos", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 68, Name = "Puerto Cortés", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 69, Name = "San Antonio de Cortés", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 70, Name = "San Francisco de Yojoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 71, Name = "San Manuel", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 72, Name = "Santa Cruz de Yojoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 73, Name = "Villanueva", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 74, Name = "La Lima", State = context.States.Last() });
            #endregion

            #region Choluteca
            context.States.AddOrUpdate(new State { Id = 6, Name = "Choluteca" });
            context.Cities.AddOrUpdate(new City { Id = 75, Name = "Choluteca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 76, Name = "Apacilagua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 77, Name = "Concepción de María", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 78, Name = "Duyure", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 79, Name = "El Corpus", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 80, Name = "El Triunfo", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 81, Name = "Marcovia", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 82, Name = "Morolica", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 83, Name = "Namasigue", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 84, Name = "Orocuina", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 85, Name = "Pespire", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 86, Name = "San Antonio de Flores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 87, Name = "San Isidro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 88, Name = "San José", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 89, Name = "San Marcos de Colón", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 90, Name = "Santa Ana de Yusguare", State = context.States.Last() });
            #endregion

            #region El Paraiso
            context.States.AddOrUpdate(new State { Id = 7, Name = "El Paraíso" });
            context.Cities.AddOrUpdate(new City { Id = 91, Name = "Yuscarán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 92, Name = "Alauca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 93, Name = "Danlí", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 94, Name = "El Paraíso", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 95, Name = "Güinope", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 96, Name = "Jacaleapa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 97, Name = "Liure", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 98, Name = "Morocelí", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 99, Name = "Oropolí", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 100, Name = "Potrerillos", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 101, Name = "San Antonio de Flores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 102, Name = "San Lucas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 103, Name = "San Matías", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 104, Name = "Soledad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 105, Name = "Teupasenti", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 106, Name = "Texiguat", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 107, Name = "Vado Ancho", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 108, Name = "Yauyupe", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 109, Name = "Trojes", State = context.States.Last() });
            #endregion

            #region Francisco Morazan
            context.States.AddOrUpdate(new State { Id = 8, Name = "Francisco Morazán" });
            context.Cities.AddOrUpdate(new City { Id = 110, Name = "Distrito Central", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 111, Name = "Alubarén", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 112, Name = "Cedros", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 113, Name = "Curarén", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 114, Name = "El Porvenir", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 115, Name = "Guaimaca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 116, Name = "La Libertad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 117, Name = "La Venta", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 118, Name = "Lepaterique", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 119, Name = "Maraita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 120, Name = "Marale", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 121, Name = "Nueva Armenia", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 122, Name = "Ojojona", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 123, Name = "Orica", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 124, Name = "Reitoca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 125, Name = "Sabanagrande", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 126, Name = "San Antonio de Oriente", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 127, Name = "San Buenaventura", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 128, Name = "San Ignacio", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 129, Name = "San Juan de Flores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 130, Name = "San Miguelito", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 131, Name = "Santa Ana", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 132, Name = "Santa Lucía", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 133, Name = "Talanga", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 134, Name = "Tatumbla", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 135, Name = "Valle de Ángeles", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 136, Name = "Villa de San Francisco", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 137, Name = "Vallecillo", State = context.States.Last() });
            #endregion

            #region Gracias a Dios
            context.States.AddOrUpdate(new State { Id = 9, Name = "Gracias a Dios" });
            context.Cities.AddOrUpdate(new City { Id = 138, Name = "Puerto Lempira", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 139, Name = "Brus Laguna", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 140, Name = "Ahuas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 141, Name = "Juan Francisco Bulnes", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 142, Name = "Ramón Villeda Morales", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 143, Name = "Wampusirpe", State = context.States.Last() });
            #endregion

            #region Intibuca
            context.States.AddOrUpdate(new State { Id = 10, Name = "Intibucá" });
            context.Cities.AddOrUpdate(new City { Id = 144, Name = "La Esperanza", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 145, Name = "Camasca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 146, Name = "Colomoncagua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 147, Name = "Concepción", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 148, Name = "Dolores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 149, Name = "Intibucá", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 150, Name = "Jesús de Otoro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 151, Name = "Magdalena", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 152, Name = "Masaguara", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 153, Name = "San Antonio", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 154, Name = "San Isidro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 155, Name = "San Juan", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 156, Name = "San Marcos de la Sierra", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 157, Name = "San Miguel Guancapla", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 158, Name = "Santa Lucía", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 159, Name = "Yamaranguila", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 160, Name = "San Francisco de Opalaca", State = context.States.Last() });
            #endregion

            #region Islas de la Bahia
            context.States.AddOrUpdate(new State { Id = 11, Name = "Islas de la Bahía" });
            context.Cities.AddOrUpdate(new City { Id = 161, Name = "Roatán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 162, Name = "Guanaja", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 163, Name = "José Santos Guardiola", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 164, Name = "Utila", State = context.States.Last() });
            #endregion

            #region La Paz
            context.States.AddOrUpdate(new State { Id = 12, Name = "La Paz" });
            context.Cities.AddOrUpdate(new City { Id = 165, Name = "La Paz", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 166, Name = "Aguanqueterique", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 167, Name = "Cabañas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 168, Name = "Cane", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 169, Name = "Chinacla", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 170, Name = "Guajiquiro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 171, Name = "Lauterique", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 172, Name = "Marcala", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 173, Name = "Mercedes de Oriente", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 174, Name = "Opatoro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 175, Name = "San Antonio del Norte", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 176, Name = "San José", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 177, Name = "San Juan", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 178, Name = "San Pedro de Tutule", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 179, Name = "Santa Ana", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 180, Name = "Santa Elena", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 181, Name = "Santa María", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 182, Name = "Santiago de Puringla", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 183, Name = "Yarula", State = context.States.Last() });
            #endregion

            #region Lempira
            context.States.AddOrUpdate(new State { Id = 13, Name = "Lempira" });
            context.Cities.AddOrUpdate(new City { Id = 184, Name = "Gracias", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 185, Name = "Belén", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 186, Name = "Candelaria", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 187, Name = "Cololaca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 188, Name = "Erandique", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 189, Name = "Gualcince", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 190, Name = "Guarita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 191, Name = "La Campa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 192, Name = "La Iguala", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 193, Name = "Las Flores", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 194, Name = "La Unión", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 195, Name = "La Virtud", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 196, Name = "Lepaera", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 197, Name = "Mapulaca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 198, Name = "Piraera", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 199, Name = "San Andrés", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 200, Name = "San Francisco", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 201, Name = "San Juan Guarita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 202, Name = "San Manuel Colohete", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 203, Name = "San Rafael", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 204, Name = "San Sebastián", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 205, Name = "Santa Cruz", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 206, Name = "Talgua", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 207, Name = "Tambla", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 208, Name = "Tomalá", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 209, Name = "Valladolid", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 210, Name = "Virginia", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 211, Name = "San Marcos de Caiquín", State = context.States.Last() });
            #endregion

            #region Ocotepeque
            context.States.AddOrUpdate(new State { Id = 14, Name = "Ocotepeque" });
            context.Cities.AddOrUpdate(new City { Id = 212, Name = "Ocotepeque", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 213, Name = "Belén Gualcho", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 214, Name = "Concepción", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 215, Name = "Dolores Merendón", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 216, Name = "Fraternidad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 217, Name = "La Encarnación", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 218, Name = "La Labor", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 219, Name = "Lucerna", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 220, Name = "Mercedes", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 221, Name = "San Fernando", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 222, Name = "San Francisco del Valle", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 223, Name = "San Jorge", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 224, Name = "San Marcos", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 225, Name = "Santa Fe", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 226, Name = "Sensenti", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 227, Name = "Sinuapa", State = context.States.Last() });
            #endregion

            #region Olancho
            context.States.AddOrUpdate(new State { Id = 15, Name = "Olancho" });
            context.Cities.AddOrUpdate(new City { Id = 228, Name = "Juticalpa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 229, Name = "Campamento", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 230, Name = "Catacamas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 231, Name = "Concordia", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 232, Name = "Dulce Nombre de Culmí", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 233, Name = "El Rosario", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 234, Name = "Esquipulas del Norte", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 235, Name = "Gualaco", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 236, Name = "Guarizama", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 237, Name = "Guata", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 238, Name = "Guayape", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 239, Name = "Jano", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 240, Name = "La Unión", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 241, Name = "Mangulile", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 242, Name = "Manto", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 243, Name = "Salamá", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 244, Name = "San Esteban", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 245, Name = "San Francisco de Becerra", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 246, Name = "San Francisco de la Paz", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 247, Name = "Santa María del Real", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 248, Name = "Silca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 249, Name = "Yocón", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 250, Name = "Patuca", State = context.States.Last() });
            #endregion

            #region Santa Barbara
            context.States.AddOrUpdate(new State { Id = 16, Name = "Santa Bárbara" });
            context.Cities.AddOrUpdate(new City { Id = 251, Name = "Santa Bárbara", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 252, Name = "Arada", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 253, Name = "Atima", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 254, Name = "Azacualpa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 255, Name = "Ceguaca", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 256, Name = "Concepción del Norte", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 257, Name = "Concepción del Sur", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 258, Name = "Chinda", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 259, Name = "El Níspero", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 260, Name = "Gualala", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 261, Name = "Ilama", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 262, Name = "Las Vegas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 263, Name = "Macuelizo", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 264, Name = "Naranjito", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 265, Name = "Nuevo Celilac", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 266, Name = "Nueva Frontera", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 267, Name = "Petoa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 268, Name = "Protección", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 269, Name = "Quimistán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 270, Name = "San Francisco de Ojuera", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 271, Name = "San José de las Colinas", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 272, Name = "San Luis", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 273, Name = "San Marcos", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 274, Name = "San Nicolás", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 275, Name = "San Pedro Zacapa", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 276, Name = "San Vicente Centenario", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 277, Name = "Santa Rita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 278, Name = "Trinidad", State = context.States.Last() });
            #endregion

            #region Valle
            context.States.AddOrUpdate(new State { Id = 17, Name = "Valle" });
            context.Cities.AddOrUpdate(new City { Id = 279, Name = "Nacaome", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 280, Name = "Alianza", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 281, Name = "Amapala", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 282, Name = "Aramecina", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 283, Name = "Caridad", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 284, Name = "Goascorán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 285, Name = "Langue", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 286, Name = "San Francisco de Coray", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 287, Name = "San Lorenzo", State = context.States.Last() });
            #endregion

            #region Yoro
            context.States.AddOrUpdate(new State { Id = 18, Name = "Yoro" });
            context.Cities.AddOrUpdate(new City { Id = 288, Name = "Yoro", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 289, Name = "Arenal", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 290, Name = "El Negrito", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 291, Name = "El Progreso", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 292, Name = "Jocón", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 293, Name = "Morazán", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 294, Name = "Olanchito", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 295, Name = "Santa Rita", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 296, Name = "Sulaco", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 297, Name = "Victoria", State = context.States.Last() });
            context.Cities.AddOrUpdate(new City { Id = 298, Name = "Yorito", State = context.States.Last() });
            #endregion
        }
    }
}
