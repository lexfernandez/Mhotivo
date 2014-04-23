using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Grade")]
    public class Grade
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        public string Name { get; set; }
        public string EducationLevel { get; set; }
    }
}