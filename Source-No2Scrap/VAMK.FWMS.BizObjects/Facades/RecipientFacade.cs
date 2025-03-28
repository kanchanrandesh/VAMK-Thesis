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
    public class RecipientFacade
    {
        public TransferObject<Recipient> Save(Recipient recipient)
        {
            var transferObject = new TransferObject<Recipient>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            if (recipient.ID == null)
            {
                recipient.State = Models.Interfaces.State.Added;
                foreach (var item in recipient.ContactPersonList)
                    item.State = Models.Interfaces.State.Added;

                var auditTrail = Resources.Utility.CreateAuditTrail(null, recipient, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetRecipientBO().Save(recipient);
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
                var dbRecipient = BizObjectFactory.GetRecipientBO().GetSingle(recipient.ID.Value);

                var childListNames = new List<string>();
                childListNames.Add("ContactPersonList");

                var auditTrail = Resources.Utility.CreateAuditTrail(dbRecipient, recipient, Models.Enums.AuditTrailAction.Update, childListNames, 0);


                dbRecipient.Code = recipient.Code;
                dbRecipient.Name = recipient.Name;
                dbRecipient.Address = recipient.Address;                
                dbRecipient.State = Models.Interfaces.State.Modified;

                foreach (var item in recipient.ContactPersonList)
                {
                    var dbContactPersonItem = dbRecipient.ContactPersonList.Where(i => i.ID == item.ID && item.ID != null && i.IsDoner == false).FirstOrDefault();
                    if (dbContactPersonItem != null)
                    {
                        dbContactPersonItem.Recipient = null;
                        dbContactPersonItem.RecipientID = item.RecipientID;
                        dbContactPersonItem.Mobile = item.Mobile;
                        dbContactPersonItem.Code = item.Code;
                        dbContactPersonItem.Name = item.Name;
                        dbContactPersonItem.PhoneNumber = item.PhoneNumber;
                        dbContactPersonItem.IsDoner = false;
                        dbContactPersonItem.Email = item.Email;
                        dbContactPersonItem.State = Models.Interfaces.State.Modified;
                    }
                    else
                    {
                        dbRecipient.ContactPersonList.Add(new ContactPerson()
                        {
                            RecipientID = item.RecipientID,
                            Mobile = item.Mobile,
                            Code = item.Code,
                            Name = item.Name,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            IsDoner = false,
                            State = Models.Interfaces.State.Added
                        }); ;
                    }
                }

                var deletedList = dbRecipient.ContactPersonList.Where(p => recipient.ContactPersonList.All(p2 => p2.ID != p.ID && p.ID != null && p2.IsDoner == false));
                foreach (var item in deletedList)
                    item.State = Models.Interfaces.State.Deleted;



                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetRecipientBO().Save(dbRecipient);
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

        public TransferObject<bool> Delete(Recipient recipient)
        {
            var transferObject = new TransferObject<bool>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            var auditTrail = Resources.Utility.CreateAuditTrail(null, recipient, Models.Enums.AuditTrailAction.Delete, new List<string>(), 0);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetRecipientBO().Delete(recipient.ID.Value);
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
