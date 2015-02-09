﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class ClassActivityGrading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual ClassActivity ClassActivity { get; set; }
        public virtual Student Student { get; set; }
        public double Score { get; set; }
        public double Percentage { get; set; }
        public string Comments { get; set; }
    }
}
