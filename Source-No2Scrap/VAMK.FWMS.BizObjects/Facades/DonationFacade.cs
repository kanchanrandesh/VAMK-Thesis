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
    public class DonationFacade
    {
        public TransferObject<Donation> Save(Donation donation, SequenceNumber sequenceNumber)
        {
            var transferObject = new TransferObject<Donation>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            if (donation.ID == null)
            {
                donation.State = Models.Interfaces.State.Added;
                
                foreach (var item in donation.DonationItemList)
                {
                    item.State = Models.Interfaces.State.Added;
                    item.ProductCode = DateTime.Now.ToLongTimeString();
                }
                var auditTrail = Resources.Utility.CreateAuditTrail(null, donation, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetDonationBO().Save(donation);
                    if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        return transferObject;

                    if (sequenceNumber != null)
                    {
                        var sequenceNumberTO = BizObjectFactory.GetSequenceNumberBO().Save(sequenceNumber);
                        if (sequenceNumberTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            transferObject.StatusInfo = sequenceNumberTO.StatusInfo;
                            return transferObject;
                        }
                    }

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
                var dbDonation = BizObjectFactory.GetDonationBO().GetSingle(donation.ID.Value);

                var childListNames = new List<string>();
                childListNames.Add("DonationItemList");

                var auditTrail = Resources.Utility.CreateAuditTrail(dbDonation, donation, Models.Enums.AuditTrailAction.Update, childListNames, 0);


                dbDonation.Date = donation.Date;
                dbDonation.TransacionNumber = donation.TransacionNumber;
                dbDonation.ManualRefNumber = donation.ManualRefNumber;
                dbDonation.Description = donation.Description;
                dbDonation.DonationSatus = donation.DonationSatus;
                dbDonation.Doner = donation.Doner;
                dbDonation.DonerID = donation.DonerID;
                dbDonation.User = donation.User;

                dbDonation.State = Models.Interfaces.State.Modified;

                foreach (var listItem in donation.DonationItemList)
                {
                    var dbDonationItem = dbDonation.DonationItemList.Where(i => i.ID == listItem.ID && listItem.ID != null).FirstOrDefault();
                    if (dbDonationItem != null)
                    {
                        dbDonationItem.Donation = null;
                        dbDonationItem.DonationID = listItem.DonationID;
                        dbDonationItem.Item = null;
                        dbDonationItem.ItemID = listItem.ItemID;
                        dbDonationItem.Qty = listItem.Qty;
                        dbDonationItem.User = listItem.User;
                        dbDonationItem.State = Models.Interfaces.State.Modified;
                        dbDonationItem.ProductCode = DateTime.Now.ToLongTimeString();
                    }
                    else
                    {
                        dbDonation.DonationItemList.Add(new DonationItem()
                        {
                            DonationID = listItem.DonationID,
                            ItemID = listItem.ItemID,
                            Qty = listItem.Qty,
                            User = listItem.User,
                            State = Models.Interfaces.State.Added
                        }); ;
                    }
                }

                var deletedList = dbDonation.DonationItemList.Where(p => donation.DonationItemList.All(p2 => p2.ID != p.ID && p.ID != null));
                foreach (var item in deletedList)
                    item.State = Models.Interfaces.State.Deleted;



                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetDonationBO().Save(dbDonation);
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

        public TransferObject<bool> Delete(Donation donation)
        {
            var transferObject = new TransferObject<bool>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            var auditTrail = Resources.Utility.CreateAuditTrail(null, donation, Models.Enums.AuditTrailAction.Delete, new List<string>(), 0);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetDonationBO().Delete(donation.ID.Value);
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
