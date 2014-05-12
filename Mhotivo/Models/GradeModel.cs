﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Grade")]
    public class Grade
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
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

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nivel Educativo")]
        public string EducationLevel { get; set; }

    }

    public class GradeRegisterModel
    {


        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nivel Educativo")]
        public string EducationLevel { get; set; }


    }





}