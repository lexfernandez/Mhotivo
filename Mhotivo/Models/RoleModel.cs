using System;
using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class RoleEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Nombre")]
        public String Name { get; set; }

        [Display(Name = "Descripci�n")]
        public String Description { get; set; }
    }
}