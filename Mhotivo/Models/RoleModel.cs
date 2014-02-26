using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mhotivo.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

    public class RoleEditModel
    {
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public String Name { get; set; }

        [Display(Name = "Descripción")]
        public String Description { get; set; }
    }
}