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
    }

    public class DisplayStudentModel
    {
        public int StudentID { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }

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

        [Display(Name = "Segundo Tutor o Padre")]
        public string Tutor2 { get; set; }
    }

    public class StudentEditModel
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