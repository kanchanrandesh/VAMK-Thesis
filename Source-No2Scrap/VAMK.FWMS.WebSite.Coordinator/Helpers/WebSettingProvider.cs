using VAMK.FWMS.Models.Util;
using System.Configuration;
using System.Web;

namespace VAMK.FWMS.WebSite.Helpers
{
    public class WebSettingProvider
    {
        private static WebSettings websettings;
        private WebSettingProvider() { }

        public static WebSettings GetWebSettings()
        {
            if(websettings == null)
            {
                var hostPath = ConfigurationManager.AppSettings["HostPath"];

                websettings = new WebSettings();
                websettings.EmailTemplates = new EmailTemplates();
                websettings.FunctionURLs = new FunctionURLs();
                websettings.EmailTemplates.UserRegistration = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.EMPLOYEE_REGISTRATION);
                websettings.EmailTemplates.DonationCollected = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.DONATION_COLLECTED);
                websettings.FunctionURLs.Login = hostPath + FunctionURLsDictionary.LOGIN;               

            }

            return websettings;
        }

    }
    
}