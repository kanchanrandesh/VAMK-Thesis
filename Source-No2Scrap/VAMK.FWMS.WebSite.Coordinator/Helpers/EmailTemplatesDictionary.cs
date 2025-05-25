using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Helpers
{
    public static class EmailTemplatesDictionary
    {
        public const string IOU_PENDING_LEAD_APPROVAL =          @"\EmailTemplates\ioupendingleadapproval.html";
        public const string IOU_PENDING_HOD_APPROVAL =           @"\EmailTemplates\ioupendinghodapproval.html";
        public const string IOU_FINANCE_APPROVED =               @"\EmailTemplates\ioufinanceapproved.html";
        public const string IOU_REJECTED =                       @"\EmailTemplates\iourejected.html";
        public const string PETTY_CASH_PENDING_LEAD_APPROVAL =   @"\EmailTemplates\pettycashpendingleadapproval.html";
        public const string PETTY_CASH_PENDING_HOD_APPROVAL =    @"\EmailTemplates\pettycashpendinghodapproval.html";
        public const string PETTY_CASH_FINANCE_APPROVED =        @"\EmailTemplates\pettycashfinanceapproved.html";
        public const string PETTY_CASH_REJECTED =                @"\EmailTemplates\pettycashrejected.html";
        public const string PETTY_CASH_CANCELLED =               @"\EmailTemplates\pettycashcancellation.html";
        public const string EMPLOYEE_REGISTRATION =              @"\EmailTemplates\employeeregistration.html";
        public const string MEDICAL_CLAIM_PENDING_HR_CHECK =     @"\EmailTemplates\medicalclaimpendinghrcheck.html";
        public const string MEDICAL_CLAIM_PENDING_HR_APPROVAL =  @"\EmailTemplates\medicalclaimpendinghrapproval.html";
        public const string MEDICAL_CLAIM_FINANCE_APPROVED =     @"\EmailTemplates\medicalclaimfinanceapproved.html";
        //public const string PETTY_CASH_PENDING_HOD_APPROVAL =   @"\EmailTemplates\pettycashpendinghodapproval.html";
        //public const string PETTY_CASH_FINANCE_APPROVED =       @"\EmailTemplates\pettycashfinanceapproved.html";
        public const string MEDICAL_CLAIM_REJECTED =             @"\EmailTemplates\medicalclaimrejected.html";
        public const string MEDICAL_CLAIM_CANCELLED =            @"\EmailTemplates\medicalclaimcancellation.html";
        public const string DONATION_COLLECTED = @"\EmailTemplates\donationcollected.html";
    }
}