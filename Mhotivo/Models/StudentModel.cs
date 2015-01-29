﻿using Mhotivo.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mhotivo.Models
{
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
        public string BirthDate { get; set; }

        [Display(Name = "Número de Identidad")]
        public string IdNumber { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Display(Name = "País")]
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
        public string StartDate { get; set; }

        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Display(Name = "Número de Cuenta")]
        public string AccountNumber { get; set; }

        [Display(Name = "Biografía")]
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

        [Required(ErrorMessage = "Debe Ingresar Nombres")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Número de Identidad")]
        [Display(Name = "Número de Identidad")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Apellidos")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Fecha de Nacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nacionalidad")]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Ciudad")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Estado")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required(ErrorMessage = "Debe Ingresar País")]
        [Display(Name = "País")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Dirección")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Foto Perfil")]
        public HttpPostedFileBase FilePicture { get; set; }

        public string UrlPicture { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Sexo")]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Fecha de Inicio")]
        [Display(Name = "Fecha de Inicio")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo de Sangre")]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un Numero de Cuenta")]
        [Display(Name = "Número de Cuenta")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Biografía")]
        [Display(Name = "Biografía")]
        public string Biography { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Padre o Tutor")]
        [Display(Name = "Padre o Tutor")]
        public Parent FirstParent { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Madre o Segundo Tutor")]
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

        [Required(ErrorMessage = "Debe Ingresar Nombres")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Número de Identidad")]
        [Display(Name = "Número de Identidad")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Apellidos")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Fecha de Nacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nacionalidad")]
        [Display(Name = "Nacionalidad")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Ciudad")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Estado")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required(ErrorMessage = "Debe Ingresar País")]
        [Display(Name = "País")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Dirección")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 10)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Foto Perfil")]
        [Display(Name = "Foto Perfil")]
        public HttpPostedFileBase FilePicture { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Sexo")]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Fecha de Inicio")]
        [Display(Name = "Fecha de Inicio")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo de Sangre")]
        [Display(Name = "Tipo de Sangre")]
        public string BloodType { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un Número de Cuenta")]
        [Display(Name = "Número de Cuenta")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Biografía")]
        [Display(Name = "Biografía")]
        public string Biography { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Padre o Tutor")]
        [Display(Name = "Padre o Tutor")]
        public Parent FirstParent { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Madre o Segundo Tutor")]
        [Display(Name = "Madre o Segundo Tutor")]
        public Parent SecondParent { get; set; }
    }
}