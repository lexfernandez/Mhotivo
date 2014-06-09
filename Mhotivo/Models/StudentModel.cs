using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mhotivo.Models
{
    public class Student : People
    {
        public DateTime StartDate { get; set; }
        public string BloodType { get; set; }
        public string AccountNumber { get; set; }
        public string Biography { get; set; }

        public virtual Parent Tutor1 { get; set; }
        public virtual Parent Tutor2 { get; set; }
        public virtual Benefactor Benefactor { get; set; }
    }

    public class DisplayStudentModel
    {
        public long Id { get; set; }

        public ICollection<ContactInformation> Contacts { get; set; }

        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Numero de Identidad")]
        public string IdNumber { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Display(Name = "Dirección Principal")]
        public string Address { get; set; }

        [Display(Name = "Foto Perfil")]
        public string UrlPicture { get; set; }

        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Codigo de Alumno")]
        public string AccountNumber { get; set; }

        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Display(Name = "Tutor o Padre")]
        public string FirstParent { get; set; }

        [Display(Name = "Segundo Tutor o Madre")]
        public string SecondParent { get; set; }
    }

    public class StudentEditModel
    {
        public long Id { get; set; }

        public ICollection<ContactInformation> Contacts { get; set; }

        public string FullName { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Numero de Identidad")]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Foto Perfil")]
        public HttpPostedFileBase FilePicture { get; set; }

        public string UrlPicture { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Codigo de Alumno")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Required]
        [Display(Name = "Padre o Tutor")]
        public Parent FirstParent { get; set; }

        [Required]
        [Display(Name = "Madre o Segundo Tutor")]
        public Parent SecondParent { get; set; }
    }

    public class StudentBenefactorEditModel
    {
        public long Id { get; set; }

        public int OldId { get; set; }

        [Display(Name = "Estudiante")]
        public int NewId { get; set; }
    }

    public class StudentRegisterModel
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
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Pais")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Departamento")]
        public string State { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Foto Perfil")]
        public HttpPostedFileBase FilePicture { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Codigo de Alumno")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Required]
        [Display(Name = "Padre o Tutor")]
        public Parent FirstParent { get; set; }

        [Required]
        [Display(Name = "Madre o Segundo Tutor")]
        public Parent SecondParent { get; set; }
    }
}