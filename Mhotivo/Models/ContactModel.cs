using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
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