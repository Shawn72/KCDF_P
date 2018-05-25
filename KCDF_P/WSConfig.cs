using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using KCDF_P.NAVWS;

namespace KCDF_P
{
    public class WSConfig
    {
        public static Portals ObjNav
        {
            get
            {
                var ws = new Portals();

                try
                {
                    var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                    ws.Credentials = credentials;
                    ws.PreAuthenticate = true;

                }
                catch (Exception ex)
                {
                    ex.Data.Clear();
                }
                return ws;
            }
        }


        public static bool MailFunction(string body, string recepient, string subject)
        {
            bool x = false;

            try
            {
                const string fromAddress = "kcdfportal@gmail.com";
                string toAddress = recepient;
                var mail = new MailMessage();
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.From = new MailAddress(fromAddress);
                mail.Body = body;
                mail.IsBodyHtml = true;
                var client = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("kcdfportal@gmail.com", "Kcdfportal@4321*"),
                    Port = 587,
                    Host = "smtp.gmail.com",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };
                client.Send(mail);
                x = true;
            }
            catch (Exception ex2)
            {
                ex2.Data.Clear();
            }
            return x;
        }
    }
}