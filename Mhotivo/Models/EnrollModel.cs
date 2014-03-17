using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Enroll")]
    public class Enroll
    {
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Student Student { get; set; }
    }
}