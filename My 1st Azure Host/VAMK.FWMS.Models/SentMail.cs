using VAMK.FWMS.Models.Enums;
using System;

namespace VAMK.FWMS.Models
{
    public class SentMail : EntityBase
    {
        public int? CreatedByID { get; set; }
        public Models.Employee CreatedBy { get; set; }
        public EmailSender Sender { get; set; }
        public string Recipient { get; set; }
        public string MailContent { get; set; }
        public string Subject { get; set; }
        public DateTime? EmailCreatedDate { get; set; }
        public Models.Enums.EmailStatus EmailStatus { get; set; }
        public string Note { get; set; }
        public int? EmailSendAttempt { get; set; }
        public DateTime? SendDate { get; set; }
    }
}
