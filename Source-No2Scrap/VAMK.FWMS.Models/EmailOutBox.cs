using VAMK.FWMS.Models.Enums;
using System;

namespace VAMK.FWMS.Models
{
    public class EmailOutBox : EntityBase
    {
        public int? CreatedByID { get; set; }
        public Employee CreatedBy { get; set; }
        public EmailSender Sender { get; set; }
        public string SenderDisplayName { get; set; }
        public string To { get; set; }
        public string Bc { get; set; }
        public string Cc { get; set; }
        public bool IsBodyHtml { get; set; }
        public string MailContent { get; set; }
        public string Subject { get; set; }
        public DateTime? EmailCreatedDate { get; set; }
        public EmailStatus EmailStatus { get; set; }
        public string Note { get; set; }
        public int? EmailSendAttempt { get; set; }

    }
}
