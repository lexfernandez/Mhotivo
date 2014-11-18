using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class AcademicYearViewData
    {
        public int Id { get; set; }

        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [Display(Name = "Course")]
        public string Course { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Section")]
        public char Section { get; set; }

        [Display(Name = "Teacher")]
        public string Meister { get; set; }

        [Display(Name = "Teacher Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "Teacher End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Schedule")]
        public string Schedule { get; set; }

        [Display(Name = "Classroom")]
        public string Room { get; set; }

        [Display(Name = "Approved")]
        public String Approved { get; set; }

        [Display(Name = "Students Limit")]
        public int Limit { get; set; }
    }

    public class AcademicYearViewManagement
    {
        public IEnumerable<AcademicYearViewData> Elements { get; set; }

        public bool CanGenerate { get; set; }

        public int CurrentYear { get; set; }
    }
}