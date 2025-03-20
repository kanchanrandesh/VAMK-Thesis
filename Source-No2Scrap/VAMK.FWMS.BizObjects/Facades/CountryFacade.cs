using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class CountryFacade
    {
        public TransferObject<Country> Save(Country country)
        {
            AuditTrail auditTrail = null;
            if (country.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, country, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbCountry = BizObjectFactory.GetCountryBO().GetSingle(country.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbCountry, country, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Country>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetCountryBO().Save(country);
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
