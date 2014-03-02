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
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Group> WithCopy { get; set; }
        public virtual ICollection<Group> WithHiddenCopy { get; set; }
        public string Subject;
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}