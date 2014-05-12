using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mhotivo.Models
{
    [Table("Student")]
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
        public long StudentID { get; set; }

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

        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Numero de Cuenta")]
        public string AccountNumber { get; set; }

        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Display(Name = "Tutor o Padre")]
        public string Tutor1 { get; set; }

        [Display(Name = "Segundo Tutor o Madre")]
        public string Tutor2 { get; set; }

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
        public string IDNumber { get; set; }

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
        [Display(Name = "Estado")]
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
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Numero de Cuenta")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Required]
        [Display(Name = "Padre o Tutor")]
        public long Tutor1Id { get; set; }

        [Required]
        [Display(Name = "Madre o Segundo Tutor")]
        public long Tutor2Id { get; set; }
    }

    public class StudentBenefactorEditModel
    {
        public long BenefactorID { get; set; }

        public int OldID { get; set; }

        [Display(Name = "Estudiante")]
        public int NewID { get; set; }
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
        public string IDNumber { get; set; }

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
        [Display(Name = "Estado")]
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
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Numero de Cuenta")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Biografia")]
        public string Biography { get; set; }

        [Required]
        [Display(Name = "Padre o Tutor")]
        public int Tutor1Id { get; set; }

        [Required]
        [Display(Name = "Madre o Segundo Tutor")]
        public int Tutor2Id { get; set; }
    }
}