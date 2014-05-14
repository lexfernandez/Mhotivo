using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Description { get; set; }
        public User Creator { get; set; }
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
}