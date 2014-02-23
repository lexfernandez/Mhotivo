using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhotivo.Models
{
    public class MessageModel
    {
        public string MessageType { get; set; }
        public string MessageTitle { get; set; }
        public string MessageContent { get; set; }
    }
}
