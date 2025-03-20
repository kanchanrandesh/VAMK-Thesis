using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class CompanyFacade
    {
        public TransferObject<Company> Save(Company company)
        {
            AuditTrail auditTrail = null;
            if (company.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, company, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbCompany = BizObjectFactory.GetCompanyBO().GetSingle(company.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbCompany, company, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Company>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetCompanyBO().Save(company);
                if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    return transferObject;

                var auditTO = BizObjectFactory.GetAuditTrailBO().Save(auditTrail);
                if (auditTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                {
                    transferObject.StatusInfo = auditTO.StatusInfo;
                    return transferObject;
                }

                scope.Complete();
            }
            return transferObject;
        }
    }
}
