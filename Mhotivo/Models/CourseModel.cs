using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Course")]
    public class Course
    {
        public string Name { get; set; }
        public virtual Area Area { get; set; }

    }
}