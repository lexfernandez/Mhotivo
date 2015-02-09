﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class AppointmentDiaryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe Ingresar el titulo")]
        public String Title { get; set; }
        [Required(ErrorMessage = "Debe Ingresar Fecha de Inicio")]
        public DateTime DateTimeScheduled { get; set; }
        public int StatusEnum { get; set; }
        [Required(ErrorMessage = "Debe Ingresar la duracion")]
        public int AppointmentLength { get; set; }
        public  Mhotivo.Data.Entities.User Creator { get; set; }
        public bool IsAproveed { get; set; }
    }

    public class EventCreate
    {
        [Required(ErrorMessage = "Debe Ingresar Fecha de Inicio")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Descripción")]
        public String Description { get; set; }
    }
}