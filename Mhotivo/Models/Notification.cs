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

        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Remitente")]
        public string From { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Destinatario")]
        public virtual string To { get; set; }

        [Required(ErrorMessage = "Debe Ingresar CC")]
        [Display(Name = "CC")]
        public virtual string WithCopyTo { get; set; }

        [Required(ErrorMessage = "Debe Ingresar BCC")]
        [Display(Name = "BCC")]
        public virtual string WithHiddenCopyTo { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Message { get; set; }

        public DateTime Created { get; set; }
    }
}