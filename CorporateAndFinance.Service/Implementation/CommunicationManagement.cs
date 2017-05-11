using CorporateAndFinance.Service.Interface;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Implementation
{
    public class CommunicationManagement : ICommunicationManagement
    {
        private static ILog logger = LogManager.GetLogger(typeof(CommunicationManagement));
        public string AttachmentFile
        { get;set; }

        public string Body
        { get; set; }

        public string Recipient
        { get; set; }

        public List<string> RecipientBCC
        { get; set; }

        public List<string> RecipientCC
        { get; set; }

        public string Sender
        { get; set; }

        public string Subject
        { get; set; }

        public string HeaderImage
        { get; set; }

        public void SendEmail()
        {
            try
            {
             
                var message = new MailMessage();
                message.From = !string.IsNullOrEmpty(Sender) ? new MailAddress(Sender) : new MailAddress("finance.portal@arthurlawrence.net", "Consulting And Finance");
                message.To.Add(new MailAddress(Recipient));
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = true;

              

                if (Body != null && Body.Trim() != "")
                {
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, MediaTypeNames.Text.Html);
                    if (!string.IsNullOrEmpty(HeaderImage))
                    {
                        LinkedResource header = new LinkedResource(HeaderImage, MediaTypeNames.Image.Jpeg);
                        header.ContentId = "header-logo";
                        htmlView.LinkedResources.Add(header);
                    }
                    message.AlternateViews.Add(htmlView);
                }


                // Adding CC recipient if any exist.
                if (RecipientCC != null && RecipientCC.Count > 0)
                {
                    foreach (var user in RecipientCC)
                    {
                        message.CC.Add(new MailAddress(user));
                    }
                }

                // Adding BCC recipient if any exist.
                if (RecipientBCC != null && RecipientBCC.Count > 0)
                {
                    foreach (var user in RecipientCC)
                    {
                        message.Bcc.Add(new MailAddress(user));
                    }
                }


                Attachment att = null;
                if (!String.IsNullOrEmpty(AttachmentFile))
                {
                    if (File.Exists(AttachmentFile))
                    {
                        att = new Attachment(AttachmentFile);
                        message.Attachments.Add(att);
                    }
                }
                logger.DebugFormat("Sending Email From Communication Class with parameters From [{0}] , To [{1}] , Subject [{2}]", message.From, message.To, message.Subject);
                var smtp = new SmtpClient();
                smtp.Send(message);
                logger.DebugFormat("Email Successfully Send From Communication");
            }

            catch (Exception ex)
            {
                logger.DebugFormat("Email Not Send From Communication");
                logger.ErrorFormat("Exception Raised : Message[{0}] Stack Trace [{1}] ", ex.Message, ex.StackTrace);
            }
        }
    }
}
