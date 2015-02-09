using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Data.Entities
{
    public class AcademicYearDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }
        
        public virtual Course Course { get; set; }
        
        
        public virtual Meister Teacher { get; set; }
        public DateTime? Schedule { get; set; }
        public String Room { get; set; }
        
    }
}