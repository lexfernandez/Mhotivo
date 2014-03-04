﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mhotivo.App_Data;
using Mhotivo.Models;
using Microsoft.Ajax.Utilities;
using System.Net.Mail;

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

                var message = new MailMessage {From = new MailAddress(notification.From)};
                message.To.Add(notification.To);
                message.CC.Add(notification.WithCopyTo);
                message.Bcc.Add(notification.WithHiddenCopyTo);
                message.Subject = notification.Subject;
                message.IsBodyHtml = true;
                message.Body = notification.Message;

                var client = new SmtpClient("smtp.gmail.com", 587)
                             {
                                 UseDefaultCredentials = true,
                                 EnableSsl = true,
                                 Credentials =
                                     new System.Net.NetworkCredential(
                                     "mhotivo@gmail.com", "abc098765"),
                                 DeliveryMethod = SmtpDeliveryMethod.Network
                             };

                client.Send(message);
   
            }
            return true;
        }
    }
}