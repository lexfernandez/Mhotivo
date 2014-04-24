using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("AcademicYear")]
    public class AcademicYear
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
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

    public class AcademicYearViewData
    {
        public int Id { get; set; }

        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [Display(Name = "Course")]
        public string Course { get; set; }

        [Display (Name = "Year")]
        public int Year { get; set; }

        [Display (Name = "Section")]
        public char Section { get; set; }

        [Display (Name = "Teacher")]
        public string Meister { get; set; }

        [Display (Name = "Teacher Start Date")]
        public string StartDate { get; set; }

        [Display (Name = "Teacher End Date")]
        public string EndDate { get; set; }

        [Display (Name = "Schedule")]
        public string Schedule { get; set; }

        [Display (Name = "Classroom")]
        public string Room { get; set; }

        [Display (Name = "Approved")]
        public String Approved { get; set; }

        [Display (Name = "Students Limit")]
        public int Limit { get; set; }

    }

    public class AcademicYearViewManagement
    {
        public IEnumerable<AcademicYearViewData> Elements { get; set; }

        public bool CanGenerate { get; set; }

        public int CurrentYear { get; set; }
    }
}