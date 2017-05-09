using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Service.Interface
{
    public interface ICommunicationManagement

    {
        string Sender { get; set; }
        string Recipient { get; set; }
        List<string> RecipientCC { get; set; }
        List<string> RecipientBCC { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string AttachmentFile { get; set; }
        string HeaderImage
        { get; set; }
        void SendEmail();
    }
}
