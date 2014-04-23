using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Mhotivo.Models
{
    [Table("Meister")]
    public class Meister : People
    {
        public string Biography { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class DisplayMeisterModel
    {
        public long MeisterID { get; set; }

        public ICollection<ContactInformation> Contacts { get; set; }

        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public string BirthDate { get; set; }

        [Display(Name = "Numero de Identidad")]
        public string IDNumber { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        public string State { get; set; }

        [Display(Name = "Dirección Principal")]
        public string Address { get; set; }

        [Display(Name = "Foto Perfil")]
        public string UrlPicture { get; set; }

        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display (Name = "Fecha de Inicio")]
        public string StartDate { get; set; }

        [Display (Name = "EndDate")]
        public string EndDate { get; set; }

        [Display (Name = "Biografia")]
        public string Biography { get; set; }
    }

    public class MeisterEditModel
    {
        public long Id { get; set; }

        public ICollection<ContactInformation> Contacts { get; set; }

        public string FullName { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Numero de Identidad")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        public string UrlPicture { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }

        [Display(Name = "Biografia")]
        public string Biography { get; set; }
    }

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
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }

        [Display(Name = "Biografia")]
        public string Biography { get; set; }
    }
}