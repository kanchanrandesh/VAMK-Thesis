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

                websettings.EmailTemplates.IOUPendingLeadApproval = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.IOU_PENDING_LEAD_APPROVAL);
                websettings.EmailTemplates.IOUPendingHodApproval = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.IOU_PENDING_HOD_APPROVAL);
                websettings.EmailTemplates.IOUFinanceApproved = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.IOU_FINANCE_APPROVED);
                websettings.EmailTemplates.IOURejected = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.IOU_REJECTED);
                websettings.EmailTemplates.PettyCashPendingLeadApproval = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.PETTY_CASH_PENDING_LEAD_APPROVAL);
                websettings.EmailTemplates.PettyCashPendingHodApproval = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.PETTY_CASH_PENDING_HOD_APPROVAL);
                websettings.EmailTemplates.PettyCashFinanceApproved = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.PETTY_CASH_FINANCE_APPROVED);
                websettings.EmailTemplates.PettyCashRejected = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.PETTY_CASH_REJECTED);
                websettings.EmailTemplates.PettyCashCancellation = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.PETTY_CASH_CANCELLED);
                websettings.EmailTemplates.EmployeeRegistration = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.EMPLOYEE_REGISTRATION);

                websettings.EmailTemplates.MedicalClaimPendingHRCheck = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.MEDICAL_CLAIM_PENDING_HR_CHECK);
                websettings.EmailTemplates.MedicalClaimCancellation = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.MEDICAL_CLAIM_CANCELLED);
                websettings.EmailTemplates.MedicalClaimPendingHRApproval = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.MEDICAL_CLAIM_PENDING_HR_APPROVAL);
                websettings.EmailTemplates.MedicalClaimRejected = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.MEDICAL_CLAIM_REJECTED);
                websettings.EmailTemplates.MedicalClaimFinanceApproved = HttpContext.Current.Server.MapPath("~" + EmailTemplatesDictionary.MEDICAL_CLAIM_FINANCE_APPROVED);

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