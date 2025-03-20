using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VAMK.FWMS.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using VAMK.FWMS.Models.Enums;
using VAMK.FWMS.BizObjects;

namespace VAMK.FWMS.WebSite.Email.Impl
{
    public class StandardEmailServer : IEmailServer
    {

        Dictionary<EmailSender, EmailSenderSetting> emailSenderSettings;

        public Dictionary<EmailSender, EmailSenderSetting> EmailSenderSettings
        {
            get
            {
                return emailSenderSettings;
            }

            set
            {
                emailSenderSettings = value;
            }
        }

        public StandardEmailServer()
        {
            EmailSenderSettings = GetEmailSenderSettings();
        }

        public async Task<EmailOutBox> SendMail(EmailOutBox email)
        {
            #region Enable Email Certificate
            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
             X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            #endregion

            EmailSenderSetting emailSenderSetting;
            EmailSenderSettings.TryGetValue(EmailSender.General, out emailSenderSetting);
            var mailMessage = GenerateEmailMessages(email);

            try
            {
                using (var mailServer = new SmtpClient(emailSenderSetting.ServerName, emailSenderSetting.Port))
                {
                    mailServer.EnableSsl = emailSenderSetting.EnableSsl;
                    mailServer.Credentials = new NetworkCredential(emailSenderSetting.EmailSenderUserName, emailSenderSetting.EmailSenderPassword);

                    await mailServer.SendMailAsync(mailMessage);
                    email.EmailStatus = EmailStatus.Sent;
                }

            }
            catch (Exception)
            {
                email.EmailStatus = EmailStatus.Failed;
                email.EmailSendAttempt = email.EmailSendAttempt++;
            }

            return email;

        }

        private MailMessage GenerateEmailMessages(EmailOutBox outBoxItem)
        {
            MailMessage msg = new MailMessage();
            EmailSenderSetting emailSenderSetting;
            EmailSenderSettings.TryGetValue(EmailSender.General, out emailSenderSetting);

            //TO
            if (!String.IsNullOrEmpty(outBoxItem.To))
            {
                string[] toAddresses = outBoxItem.To.Split(',');
                foreach (var item in toAddresses)
                {
                    msg.To.Add(item);
                }
            }
            //BC
            if (!String.IsNullOrEmpty(outBoxItem.Bc))
            {
                string[] bcAddress = outBoxItem.Bc.Split(',');
                foreach (var item in bcAddress)
                {
                    msg.Bcc.Add(item);

                }
            }
            //CC
            if (!String.IsNullOrEmpty(outBoxItem.Cc))
            {
                string[] ccAddress = outBoxItem.Cc.Split(',');
                foreach (var item in ccAddress)
                {
                    msg.CC.Add(item);
                }
            }

            msg.Subject = outBoxItem.Subject;
            msg.Body = outBoxItem.MailContent;
            msg.IsBodyHtml = outBoxItem.IsBodyHtml;

            msg.Sender = new MailAddress(emailSenderSetting.EmailSenderUserName, emailSenderSetting.EmailSenderDisplayName);
            msg.From = new MailAddress(emailSenderSetting.EmailSenderUserName, emailSenderSetting.EmailSenderDisplayName);

            return msg;
        }

        public Dictionary<EmailSender, EmailSenderSetting> GetEmailSenderSettings()
        {
            EmailSenderSettings = new Dictionary<EmailSender, EmailSenderSetting>();

            var configuration = BizObjectFactory.GetSystemConfigurationBO().GetAll().FirstOrDefault();

            //General
            EmailSenderSettings.Add(EmailSender.General, new EmailSenderSetting()
            {
                ServerName = !String.IsNullOrEmpty(configuration.EmailServer) ? configuration.EmailServer : "",
                Port = configuration.EmailServerPort.HasValue ? configuration.EmailServerPort.Value : 0,
                EnableSsl = true,
                EmailSenderDisplayName = !String.IsNullOrEmpty(configuration.EmailSenderGeneralDisplayName) ? configuration.EmailSenderGeneralDisplayName : "",
                EmailSenderUserName = !String.IsNullOrEmpty(configuration.EmailSenderGeneral) ? configuration.EmailSenderGeneral : "",
                EmailSenderPassword = !String.IsNullOrEmpty(configuration.EmailSenderGeneralPassword) ? configuration.EmailSenderGeneralPassword : ""
            });

            return EmailSenderSettings;
        }
    }

    public class EmailSenderSetting
    {
        public string Description { get; set; }
        public string ServerName { get; set; }
        public int Port { get; set; }
        public string Connectionsecurity { get; set; }
        public string Authentication { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailSenderUserName { get; set; }
        public string EmailSenderPassword { get; set; }
        public string EmailSenderDisplayName { get; set; }

    }

}