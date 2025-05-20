using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class UnitFacade
    {
        public TransferObject<Unit> Save(Unit unit)
        {
            AuditTrail auditTrail = null;
            if (unit.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, unit, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbUnit = BizObjectFactory.GetUnitBO().GetSingle(unit.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbUnit, unit, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Unit>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetUnitBO().Save(unit);
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
