using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mhotivo.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string EventName { get; set; }
        public string From { get; set; }
        public virtual string To { get; set; }
        public virtual string WithCopyTo { get; set; }
        public virtual string WithHiddenCopyTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}