#region © 2025 Copyright Kanchan Randesh
//
// All rights are reserved. Reproduction or transmission in
// whole or in part, in any form or by any means, electronic,
// mechanical or otherwise, is prohibited without the prior
// written consent of the copyright owner.
#endregion

using VAMK.FWMS.BizObjects;
using VAMK.FWMS.WebSite.Email;
using VAMK.FWMS.WebSite.Email.Impl;
using Quartz;

namespace VAMK.FWMS.WebSite
{
    public class FWMSService : IJob
    {
        EmailClientFacade facade;

        public void Execute(IJobExecutionContext context)
        {

            facade = new EmailClientFacade(new StandardEmailServer());

            var email = BizObjectFactory.GetEmailOutBoxBO().GetAll();
            foreach (var item in email)
            {
                facade.SendEmail(item);
            }
        }
    }
}