using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.Models;
using VAMK.FWMS.WebSite.Email.Impl;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.WebSite.Email
{
    public class EmailClientFacade
    {
        IEmailServer emailServer;
        EmailItemsFacade emailItemsFacade;

        public EmailClientFacade(IEmailServer emailServer)
        {
            this.emailServer = emailServer;
            emailItemsFacade = new EmailItemsFacade(null);
        }

        public void SendEmail(EmailOutBox outboxItem)
        {
            try
            {
                emailServer.SendMail(outboxItem).ContinueWith(i =>
                {

                    if (i.Result.EmailStatus == VAMK.FWMS.Models.Enums.EmailStatus.Sent)
                    {
                        emailItemsFacade.MoveEmailToSentList(new List<int> { outboxItem.ID.Value });
                    }
                    else
                    {
                        emailItemsFacade.UpdateEmailOutBoxStatus(outboxItem);
                    }
                });

            }
            catch (Exception)
            {
            }
        }
    }
}