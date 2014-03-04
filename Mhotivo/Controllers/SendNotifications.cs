using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mhotivo.App_Data;
using Mhotivo.Models;
using Microsoft.Ajax.Utilities;

namespace Mhotivo.Controllers
{
    public class SendNotifications
    {
        MhotivoContext db = new MhotivoContext();
        public bool Send(string notificationName, Object templateParameters)
        {
            Notification notification = db.Notifications.FirstOrDefault(x => x.EventName.Equals(notificationName));
            if (notification != null)
            {
                string[] parametersList = templateParameters.GetType().GetProperties().Select(p => p.Name).ToArray();
                foreach (var parameters in parametersList)
                {
                    if (notification.Subject.Contains("${" + parameters + "}"))
                    {
                        string value = templateParameters.GetType().GetProperty(parameters).GetValue(templateParameters, null).ToString();
                        notification.Subject=notification.Subject.Replace("${" + parameters + "}", value);
                    }
                    if (notification.Message.Contains("${" + parameters + "}"))
                    {
                        string value = templateParameters.GetType().GetProperty(parameters).GetValue(templateParameters, null).ToString();
                        notification.Message = notification.Message.Replace("${" + parameters + "}", value);
                    }
                }
   
            }
            return true;
        }
    }
}