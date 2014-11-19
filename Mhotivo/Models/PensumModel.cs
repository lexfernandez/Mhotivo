using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class DisplayPensumModel
    {
        public int Id { get; set; }

        [Display(Name = "Curso")]
        public string Course { get; set; }

        [Display(Name = "Grado")]
        public string Grade { get; set; }
    }

    public class PensumRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar Curso")]
        [Display(Name = "Curso")]
        public int IdCourse { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Grado")]
        [Display(Name = "Grado")]
        public int IdGrade { get; set; }

    }

    public class PensumEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Curso")]
        [Display(Name = "Curso")]
        public int IdCourse { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Grado")]
        [Display(Name = "Grado")]
        public int IdGrade { get; set; }
    }
}