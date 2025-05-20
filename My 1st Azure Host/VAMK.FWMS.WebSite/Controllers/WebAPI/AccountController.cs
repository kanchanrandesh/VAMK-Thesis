using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Models;
using VAMK.FWMS.WebSite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(PasswordUpdateModel password)
        {
            var transObject = new AccountFacade().ChangePassword(new SessionSetting().EmployeeDetail.ID.Value,password.currentPassword, password.newPassword,password.conformPassword);
            return Ok(transObject.StatusInfo);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword(PasswordReset userName)
        {
            var transObject = new AccountFacade().ResetPassword(userName.userName);
            return Ok(transObject.StatusInfo);
        }
    }
}