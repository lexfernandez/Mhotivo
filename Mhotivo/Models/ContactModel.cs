using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Mhotivo.Models
{
    [Table("ContactInformation")]
    public class ContactInformation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public People People { get; set; }
    }

    public class ContactInformationEditModel
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        public People people { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Valor")]
        public string Value { get; set; }
    }

    public class ContactInformationRegisterModel
    {
        public int PeopleId { get; set; }

        public string Controller { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Valor")]
        public string Value { get; set; }
    }
}