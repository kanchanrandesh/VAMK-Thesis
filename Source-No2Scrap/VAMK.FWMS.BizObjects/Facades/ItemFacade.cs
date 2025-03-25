using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class ItemFacade
    {
        public TransferObject<Item> Save(Item item)
        {
            AuditTrail auditTrail = null;
            if (item.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, item, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbItem = BizObjectFactory.GetItemBO().GetSingle(item.ID.Value);
                auditTrail = Resources.Utility.CreateAuditTrail(dbItem, item, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<Item>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetItemBO().Save(item);
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
