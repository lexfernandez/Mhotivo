using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Mhotivo.Models
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class AddGroup
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        public string Users { get; set; }
    }
}