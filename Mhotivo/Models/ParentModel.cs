using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mhotivo.Models
{
    [Table("Parent")]
    public class Parent : People
    {
        public string JustARandomColumn { get; set; }
    }

    public class DisplayParentModel
    {
        public int ParentID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Estudiantes")]
        public string Students { get; set; }
    }

    public class ParentEditModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Numero de Identidad")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public string StreetAddress { get; set; }
    }

    public class ParentRegisterModel
    {
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Numero de Identidad")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public string StreetAddress { get; set; }
    }
}