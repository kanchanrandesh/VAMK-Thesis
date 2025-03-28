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
    public class DonerFacade
    {
        public TransferObject<Doner> Save(Doner doner)
        {
            var transferObject = new TransferObject<Doner>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            if (doner.ID == null)
            {
                doner.State = Models.Interfaces.State.Added;
                foreach (var item in doner.ContactPersonList)
                    item.State = Models.Interfaces.State.Added;

                var auditTrail = Resources.Utility.CreateAuditTrail(null, doner, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetDonerBO().Save(doner);
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
            }
            else
            {
                var dbDoner = BizObjectFactory.GetDonerBO().GetSingle(doner.ID.Value);

                var childListNames = new List<string>();
                childListNames.Add("ContactPersonList");

                var auditTrail = Resources.Utility.CreateAuditTrail(dbDoner, doner, Models.Enums.AuditTrailAction.Update, childListNames, 0);


                dbDoner.Code = doner.Code;
                dbDoner.Name = doner.Name;
                dbDoner.Address = doner.Address;
                dbDoner.Location = doner.Location;
                dbDoner.State = Models.Interfaces.State.Modified;

                foreach (var item in doner.ContactPersonList)
                {
                    var dbContactPersonItem = dbDoner.ContactPersonList.Where(i => i.ID == item.ID && item.ID != null && i.IsDoner == true).FirstOrDefault();
                    if (dbContactPersonItem != null)
                    {
                        dbContactPersonItem.Doner = null;
                        dbContactPersonItem.DonerID = item.DonerID;
                        dbContactPersonItem.Mobile = item.Mobile;
                        dbContactPersonItem.Code = item.Code;
                        dbContactPersonItem.Name = item.Name;
                        dbContactPersonItem.PhoneNumber = item.PhoneNumber;
                        dbContactPersonItem.IsDoner = true;
                        dbContactPersonItem.Email = item.Email;
                        dbContactPersonItem.State = Models.Interfaces.State.Modified;
                    }
                    else
                    {
                        dbDoner.ContactPersonList.Add(new ContactPerson()
                        {
                            Code = item.Code,
                            DonerID = item.DonerID,
                            Mobile = item.Mobile,
                            Name = item.Name,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            IsDoner = true,
                            State = Models.Interfaces.State.Added
                        }); ;
                    }
                }

                var deletedList = dbDoner.ContactPersonList.Where(p => doner.ContactPersonList.All(p2 => p2.ID != p.ID && p.ID != null && p2.IsDoner == true));
                foreach (var item in deletedList)
                    item.State = Models.Interfaces.State.Deleted;



                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetDonerBO().Save(dbDoner);
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
            }

            return transferObject;
        }

        public TransferObject<bool> Delete(Doner doner)
        {
            var transferObject = new TransferObject<bool>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            var auditTrail = Resources.Utility.CreateAuditTrail(null, doner, Models.Enums.AuditTrailAction.Delete, new List<string>(), 0);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetDonerBO().Delete(doner.ID.Value);
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
