using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class GroupFacade
    {
        public TransferObject<Group> Save(Group group)
        {
            AuditTrail auditTrail = null;
            if (group.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, group, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbGroup = BizObjectFactory.GetGroupBO().GetSingle(group.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbGroup, group, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Group>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetGroupBO().Save(group);
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

        public TransferObject<GroupEmployee> SaveUsers(List<GroupEmployee> newList, List<GroupEmployee> deleteList)
        {
            var transferObject = new TransferObject<GroupEmployee>();
            var transferDeleteObject = new TransferObject<bool>();

            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                foreach (var item in newList)
                {
                    transferObject = BizObjectFactory.GetGroupEmployeeBO().Save(item);
                    if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        return transferObject;
                }

                foreach (var item in deleteList)
                {
                    transferDeleteObject = BizObjectFactory.GetGroupEmployeeBO().Delete(item.ID.Value);
                    if (transferDeleteObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    {
                        transferObject.StatusInfo = transferDeleteObject.StatusInfo;
                        return transferObject;
                    }
                    transferObject.StatusInfo = transferDeleteObject.StatusInfo;
                }

                scope.Complete();
            }
            return transferObject;
        }

        public TransferObject<GroupRule> SaveRules(List<GroupRule> newList, List<GroupRule> deleteList)
        {
            var transferObject = new TransferObject<GroupRule>();
            var transferDeleteObject = new TransferObject<bool>();

            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                foreach (var item in newList)
                {
                    transferObject = BizObjectFactory.GetGroupRuleBO().Save(item);
                    if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        return transferObject;
                }

                foreach (var item in deleteList)
                {
                    transferDeleteObject = BizObjectFactory.GetGroupRuleBO().Delete(item.ID.Value);
                    if (transferDeleteObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    {
                        transferObject.StatusInfo = transferDeleteObject.StatusInfo;
                        return transferObject;
                    }
                    transferObject.StatusInfo = transferDeleteObject.StatusInfo;
                }

                scope.Complete();
            }
            return transferObject;
        }
    }
}
