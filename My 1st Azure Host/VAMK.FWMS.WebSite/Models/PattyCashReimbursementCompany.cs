using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JitSys.WebSite.Models
{
    public class PattyCashReimbursementCompany
    {
        public  string ReimbursementAmount { get; set; }
        public string CompanyName { get; set; }
        public bool EligibleForPettyCashReimbursmentoperty { get; set; }

        public static  explicit operator PattyCashReimbursementCompany(JitSys.Models.Company e)
        {
            return new PattyCashReimbursementCompany()
            {
                CompanyName = e.Code,
                ReimbursementAmount = e.PettyCashCurrentTotal.ToString("N2"),
                EligibleForPettyCashReimbursmentoperty = e.IsEligibleForPettyCashReimbursment
            };
        }
    }

}