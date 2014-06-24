using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class ContactInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }
        public People People { get; set; }
    }

    public class ContactInformationEditModel
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        public People People { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo")]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Valor")]
        [Display(Name = "Valor")]
        public string Value { get; set; }
    }

    public class ContactInformationRegisterModel
    {
        public int Id { get; set; }

        public string Controller { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo")]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Valor")]
        [Display(Name = "Valor")]
        public string Value { get; set; }
    }
}