using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Pensum")]
    public class Pensum
    {
        public virtual Course Course { get; set; }
        public virtual Grade Grade { get; set; }
    }
}