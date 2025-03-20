using VAMK.FWMS.BizObjects;
using VAMK.FWMS.Common.Enums;
using VAMK.FWMS.Models;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Integrations.Helpers.Filters;
using VAMK.FWMS.WebSite.Integrations.Mobile.Helpers;
using VAMK.FWMS.WebSite.Integrations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.API
{
    [RoutePrefix("integration/login")]
    public class LoginApiController : ApiController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [AllowAnonymous]
        [HttpPost]
        [Route("getToken")]
        public RequestStatus GetToken(LoginViewModel loginViewModel)
        {
            log.Info(Serializer.GetPostData(this).Result);
            RequestStatus loginStatus = new RequestStatus();
            if (loginViewModel != null)
            {
                if (!string.IsNullOrEmpty(loginViewModel.UserName) && !string.IsNullOrEmpty(loginViewModel.Password))
                {
                    Employee user = BizObjectFactory.GetEmployeeBO().ValidateLoginCredential(loginViewModel.UserName, loginViewModel.Password);

                    if (user != null && user.ID > 0)
                    {
                        if (!user.IsLocked)
                        {
                            loginStatus.Token = JwtManager.GenerateToken(user.ID.Value.ToString());
                            loginStatus.Status = ServiceStatus.Success.ToString();
                        }
                        else
                        {
                            loginStatus.Status = ServiceStatus.AuthenticationFailed.ToString();
                            loginStatus.Message = "User Account is Locked";
                        }
                    }
                    else
                    {
                        loginStatus.Status = ServiceStatus.AuthenticationFailed.ToString();
                        loginStatus.Message = "Invalid User Name or Password / In Active User Account";
                    }
                }
                else
                {
                    loginStatus.Status = ServiceStatus.AuthenticationFailed.ToString();
                    loginStatus.Message = "User Name or Password cannot be blank";
                }
            }
            else
            {
                loginStatus.Status = ServiceStatus.AuthenticationFailed.ToString();
                loginStatus.Message = "Input Parameters cannot be blank";
            }

            log.Info("Response : " + Serializer.GetObjectJson(loginStatus));
            return loginStatus;
        }

        [JwtAuthentication]
        [HttpGet]
        [Route("getLoginUser")]
        public LoginViewModel GetLoginUser()
        {
            log.Info(Serializer.GetPostData(this).Result);

            LoginViewModel returnLogin = new LoginViewModel();

            var caller = User as ClaimsPrincipal;
            var userID = caller.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            if (!String.IsNullOrEmpty(userID))
            {
                var employee = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userID));
                if (employee != null)
                {
                    returnLogin.ID = employee.ID;
                    returnLogin.UserName = employee.UserName;
                    returnLogin.FirstName = employee.FirstName;
                    returnLogin.LastName = employee.LastName;
                    returnLogin.Status = ServiceStatus.Success.ToString();
                }
                else
                {
                    returnLogin.Status = ServiceStatus.DataNotAvailable.ToString();
                    returnLogin.Message = "User is not available";
                }
            }
            else
            {
                returnLogin.Status = ServiceStatus.DataNotAvailable.ToString();
                returnLogin.Message = "User ID is not available";
            }

            log.Info("Response : " + Serializer.GetObjectJson(returnLogin));
            return returnLogin;
        }
    }
}