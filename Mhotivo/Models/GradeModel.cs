using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EducationLevel { get; set; }
    }


    public class DisplayGradeModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Nivel Educativo")]
        public string EducationLevel { get; set; }
    }

    public class GradeEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nivel Educativo")]
        [Display(Name = "Nivel Educativo")]
        public string EducationLevel { get; set; }
    }

    public class GradeRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nivel Educativo")]
        [Display(Name = "Nivel Educativo")]
        public string EducationLevel { get; set; }
    }
}