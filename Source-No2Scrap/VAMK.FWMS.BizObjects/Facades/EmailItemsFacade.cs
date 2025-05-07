using VAMK.FWMS.BizObjects.Impl;
using VAMK.FWMS.BizObjects.Impl.EmailTemplate;
using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Enums;
using VAMK.FWMS.Models.Util;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class EmailItemsFacade
    {
        private WebSettings websettings;
        public EmailItemsFacade(WebSettings websettings)
        {
            this.websettings = websettings;
        }
        public TransferObject<EmailOutBox> AddEmailToOutBox(EmailOutBox email)
        {
            var transferObject = new TransferObject<EmailOutBox>();
            transferObject = BizObjectFactory.GetEmailOutBoxBO().Save(email);

            return transferObject;
        }

        public bool MoveEmailToSentList(List<int> emailIds)
        {
            var outboxItems = BizObjectFactory.GetEmailOutBoxBO().GetAll();
            //var transferObject = new TransferObject<EventParticipant>();           
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                foreach (var item in outboxItems)
                {
                    if (emailIds.Contains(item.ID.Value))
                    {
                        item.State = Models.Interfaces.State.Deleted;
                        BizObjectFactory.GetEmailOutBoxBO().Save(item);

                        SentMail sentMail = ConvertEmailOutBoxItem(item);
                        sentMail.SendDate = DateTime.Now;
                        sentMail.State = Models.Interfaces.State.Added;
                        BizObjectFactory.GetSentEmailBO().Save(sentMail);
                    }

                }

                scope.Complete();
            }

            return true;
        }

        public string GenerateEmailcontentFromTemplate<T>(EmailTemplate emailTemplate, T data)
        {
            IEmailTemplateReader reader = new FileEmailTemplateReader();
            ITemplateContentGenerator pter = new StandardEmailTemplate(getEmailTemplateActualPath(emailTemplate), reader);
            foreach (var item in data.GetType().GetProperties())
            {
                Object dataObject = item.GetValue(data);
                string strData = String.Empty;
                if (dataObject != null)
                {
                    strData = dataObject.ToString();
                }
                pter.PlaceHolders.Add(item.Name, strData);
            }
            return pter.GenerateContent();
        }

        public void UpdateEmailOutBoxStatus(EmailOutBox outboxItem)
        {
            try
            {
                outboxItem.State = Models.Interfaces.State.Modified;
                BizObjectFactory.GetEmailOutBoxBO().Save(outboxItem);
            }
            catch (Exception)
            {

            }
        }

        private SentMail ConvertEmailOutBoxItem(EmailOutBox item)
        {
            return new SentMail()
            {
                //AttachmentPath = item.AttachmentPath,
                //CreatedBy = item.CreatedBy,
                CreatedByID = item.CreatedByID,
                DateCreated = item.DateCreated,
                DateModified = item.DateModified,
                EmailCreatedDate = item.EmailCreatedDate,
                EmailSendAttempt = item.EmailSendAttempt,
                EmailStatus = item.EmailStatus,
                MailContent = item.MailContent,
                Note = item.Note,
                Recipient = item.To,
                //RecipientBcc = item.RecipientBcc,
                //RecipientCc = item.RecipientCc,
                //ReplayTo = item.ReplayTo,
                SendDate = DateTime.Now,
                Sender = item.Sender,
                State = item.State,
                Subject = item.Subject,
                User = item.User
            };
        }

        private string getEmailTemplateActualPath(EmailTemplate emailTemplate)
        {
            switch (emailTemplate)
            {
                case EmailTemplate.USER_REGISTRATION:
                    return this.websettings.EmailTemplates.UserRegistration;

                case EmailTemplate.DONATION_COLLECTED:
                    return this.websettings.EmailTemplates.DonationCollected;

                default:
                    return "";
            }
        }
    }
}
