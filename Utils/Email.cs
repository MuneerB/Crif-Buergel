using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace CrifAustria.Utils
{
    public class Email
        {
            public static void sendEmail(string from, string to, string subject, string body, string replyto = null, bool bHtml = false, string cc = null, string bcc = null)
            {
                List<string> toMembers = new List<string>();
                toMembers = to.Split(',').ToList();
                MailMessage email = new MailMessage(from, toMembers.First(), subject, body);
                email.IsBodyHtml = bHtml;
                foreach (string str in toMembers.Skip(1))
                {
                    email.To.Add(str);
                }

                if (replyto != null && !string.IsNullOrEmpty(replyto))
                    email.ReplyToList.Add(replyto);

                // Add a carbon copy recipient. [to remove]
                if (cc != null && !string.IsNullOrEmpty(cc))
                    email.CC.Add(new MailAddress(cc));
                if (bcc != null && !string.IsNullOrEmpty(bcc))
                {
                    List<string> BccMembers = new List<string>();
                    BccMembers = bcc.Split(',').ToList();
                    foreach (string bccmember in BccMembers)
                    {
                        email.Bcc.Add(new MailAddress(bccmember));
                    }
                }

                try
                {
                    //Connect to SMTP credentials set in web.config
                    SmtpClient smtp = new SmtpClient();
                    //SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpIp"], int.Parse(ConfigurationManager.AppSettings["smtpPort"]));
                    //if ((ConfigurationManager.AppSettings["smtpUser"] != "") && (ConfigurationManager.AppSettings["smtpPasword"] != ""))
                    //    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPasword"]);
                    //Try & send the email with the SMTP settings
                    smtp.Send(email);
                }
                catch (Exception ex)
                {
                    //Throw an exception if there is a problem sending the email
                    throw ex;
                }

            }
            public static void sendEmail(string from, List<string> to, string subject, string body, string replyto = null, bool bHtml = false, string cc = null, string bcc = null)
            {

                MailMessage email = new MailMessage(from, to.First(), subject, body);
                email.IsBodyHtml = bHtml;

                foreach (string str in to.Skip(1))
                {
                    email.To.Add(str);
                }

                if (replyto != null && !string.IsNullOrEmpty(replyto))
                    email.ReplyToList.Add(replyto);

                // Add a carbon copy recipient. [to remove]
                if (cc != null && !string.IsNullOrEmpty(cc))
                    email.CC.Add(new MailAddress(cc));

                if (bcc != null && !string.IsNullOrEmpty(bcc))
                    email.Bcc.Add(new MailAddress(bcc));

                try
                {
                    //Connect to SMTP credentials set in web.config
                    SmtpClient smtp = new SmtpClient();
                    //SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpIp"], int.Parse(ConfigurationManager.AppSettings["smtpPort"]));
                    //if ((ConfigurationManager.AppSettings["smtpUser"] != "") && (ConfigurationManager.AppSettings["smtpPasword"] != ""))
                    //    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPasword"]);
                    //Try & send the email with the SMTP settings
                    smtp.Send(email);
                }
                catch (Exception ex)
                {
                    //Throw an exception if there is a problem sending the email
                    throw ex;
                }

            }
        }
}