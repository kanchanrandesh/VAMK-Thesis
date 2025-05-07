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
            if (websettings == null)
            {
                var hostPath = ConfigurationManager.AppSettings["HostPath"];

                websettings = new WebSettings();
                websettings.EmailTemplates = new EmailTemplates();
                websettings.FunctionURLs = new FunctionURLs();                               
                websettings.EmailTemplates.UserRegistration = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.EMPLOYEE_REGISTRATION);               
                websettings.EmailTemplates.DonationCollected = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.DONATION_COLLECTED);
                websettings.FunctionURLs.Login = hostPath + FunctionURLsDictionary.IOU_REQUEST;
                websettings.FunctionURLs.IOURequest = hostPath + FunctionURLsDictionary.IOU_REQUEST;
                websettings.FunctionURLs.IOUApproval = hostPath + FunctionURLsDictionary.IOU_REQUEST;
                websettings.FunctionURLs.PettyCashRequest = hostPath + FunctionURLsDictionary.PETTY_CASH_REQUEST;
                websettings.FunctionURLs.PettyCashApproval = hostPath + FunctionURLsDictionary.PETTY_CASH_APPROVAL;
                websettings.FunctionURLs.MedicalClaimRequest = hostPath + FunctionURLsDictionary.MEDICAL_CLAIM_REQUEST;
                websettings.FunctionURLs.MedicalClaimApproval = hostPath + FunctionURLsDictionary.MEDICAL_CLAIM_APPROVAL;

            }

            return websettings;
        }

    }

}