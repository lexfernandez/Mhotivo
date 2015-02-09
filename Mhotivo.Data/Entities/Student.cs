﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class Student : People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StartDate { get; set; }
        public string BloodType { get; set; }
        public string AccountNumber { get; set; }
        public string Biography { get; set; }
        public virtual Parent Tutor1 { get; set; }
        public virtual Parent Tutor2 { get; set; }
        public virtual Benefactor Benefactor { get; set; }
    }
}