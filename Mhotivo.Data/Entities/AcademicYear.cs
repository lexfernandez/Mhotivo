using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class AcademicYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Course Course { get; set; }
        public DateTime Year { get; set; }
        public char Section { get; set; }
        public virtual Meister Teacher { get; set; }
        public DateTime? TeacherStartDate { get; set; }
        public DateTime? TeacherEndDate { get; set; }
        public DateTime? Schedule { get; set; }
        public String Room { get; set; }
        public bool Approved { get; set; }
        public bool IsActive { get; set; }
        public int StudentsLimit { get; set; }
        public int StudentsCount { get; set; }
    }
}