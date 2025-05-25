using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.WebSite.Email.Impl
{
    public interface IEmailServer
    {
        Dictionary<EmailSender, EmailSenderSetting> EmailSenderSettings { get; set; }
        Task<EmailOutBox> SendMail(EmailOutBox email);
    }
}
