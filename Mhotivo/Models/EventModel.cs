using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class EventCreate
    {
        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "Fecha de Finalizacion")]
        public DateTime EndDateTime { get; set; }
        [Required]
        public String Description { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    /*[Table("People")]
    public class Student : People
    {
        public DateTime StartDate;
        public String BloodType;
    }

    [Table("People")]
    public class People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeopleId { get; set; }
        public String IdNumber { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public String Nationality { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String StreetAddress { get; set; }
        public String PhotoDir { get; set; }
        public Gender Gender { get; set; }
        public String Description { get; set; }
    }*/
}