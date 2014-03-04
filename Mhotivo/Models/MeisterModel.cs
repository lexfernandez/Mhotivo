using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Mhotivo.Models
{

    [Table("Meister")]
    public class Meister : People
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Biography { get; set; }

    }

    //Clase DisplayMeisterModel

    public class DisplayMeisterModel
    {
        public int MeisterID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }


    }

    //Clase MeisterEditModel

    public class MeisterEditModel
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
        public string  Gender { get; set; }

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

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDate { get; set; }

        [Required] 
        [Display(Name="Biografía")]
        public String Biography { get; set; }
        

    }

    //clase MeisterRegisterModel

    public class MeisterRegisterModel
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
        public string Gender { get; set; }

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

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Biografía")]
        public String Biography { get; set; }
    }
    
}