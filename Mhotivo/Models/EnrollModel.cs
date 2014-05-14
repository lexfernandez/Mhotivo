using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    public class Enroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Student Student { get; set; }
    }

    public class DisplayEnrollStudents
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Display(Name = "Foto Perfil")]
        public string UrlPicture { get; set; }

        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Display(Name = "Numero de Cuenta")]
        public string AccountNumber { get; set; }

        [Display(Name = "Grado")]
        public string Grade { get; set; }

        [Display(Name = "Seccion")]
        public char Section { get; set; }
    }

    public class EnrollRegisterModel
    {
        [Display(Name = "Grado")]
        public int GradeId { get; set; }

        [Display(Name = "Estudiante")]
        public int Id { get; set; }
    }
}