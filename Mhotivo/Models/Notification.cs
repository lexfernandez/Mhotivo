using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Mhotivo.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string EventName { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public virtual string To { get; set; }

        [Required]
        [Display(Name = "CC")]
        public virtual string WithCopyTo { get; set; }

        [Required]
        [Display(Name = "BCC")]
        public virtual string WithHiddenCopyTo { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Message { get; set; }

        public DateTime Created { get; set; }
    }
}