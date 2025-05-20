using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class DepartmentFacade
    {
        public TransferObject<Department> Save(Department department)
        {
            AuditTrail auditTrail = null;
            if (department.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, department, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbDepartment = BizObjectFactory.GetDepartmentBO().GetSingle(department.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbDepartment, department, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Department>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetDepartmentBO().Save(department);
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
