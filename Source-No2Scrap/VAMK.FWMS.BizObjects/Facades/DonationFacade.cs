using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VAMK.FWMS.BizObjects.Impl;
using VAMK.FWMS.Models.Util;
using iTextSharp.text;
using VAMK.FWMS.Models.Enums;
using VAMK.FWMS.Models.MessageModels;
using Microsoft.Office.Interop.Excel;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class DonationFacade
    {
        WebSettings websettings;

        public DonationFacade(WebSettings websettings)
        {
            this.websettings = websettings;
        }
        public TransferObject<Donation> Save(Donation donation, SequenceNumber sequenceNumber)
        {
            var CoordinatorIntentoryItems = new List<CoordinatorIntentoryItem>();
            var InventoryStocks = new List<InventoryStock>();

            var pendingRequests = BizObjectFactory.GetRequestBO().Search(new Models.SearchQueries.RequestSearchQuery { RequestStatus = RequestStatus.AllocationPending });
            if (donation.DonationSatus == Models.Enums.DonationSatus.Collected)
            {
                foreach (var item in donation.DonationItemList)
                {
                    CoordinatorIntentoryItems.Add(new CoordinatorIntentoryItem
                    {
                        Date = DateTime.Now,
                        DateCreated = DateTime.Now,
                        EffectedQty = item.Qty,
                        InventoryEffectedby = Models.Enums.InventoryEffectedby.Donation,
                        EffectedTransacionID = donation.ID,
                        ItemID = item.ItemID,
                        User = donation.User,
                        State = Models.Interfaces.State.Added,
                        AuditReference = item.ItemID.ToString()
                    });
                    var inventoryStockDbObj = BizObjectFactory.GetInventoryStockBO().FindItemStock(item.ItemID.Value);
                    if (inventoryStockDbObj == null)
                    {
                        InventoryStocks.Add(new InventoryStock
                        {
                            ItemID = item.ItemID,
                            Quantity = item.Qty,
                            State = Models.Interfaces.State.Added,
                            User = donation.User,
                            DateCreated = DateTime.Now,
                        });
                    }
                    else
                    {
                        inventoryStockDbObj.State = Models.Interfaces.State.Modified;
                        inventoryStockDbObj.Quantity += item.Qty;
                        inventoryStockDbObj.User = donation.User;
                        inventoryStockDbObj.DateModified = DateTime.Now;
                        InventoryStocks.Add(inventoryStockDbObj);
                    }

                    foreach (var pendingRequest in pendingRequests)
                    {
                        if (inventoryStockDbObj.Quantity > 0)
                        {
                            var RequestItem = pendingRequest.RequestItemList.Where(x => x.ItemID == item.ItemID).FirstOrDefault();
                            if (RequestItem != null)
                            {
                                decimal usedQty = 0;
                                var balansQty = RequestItem.RequestedQty - RequestItem.AllocatedQty;
                                if (inventoryStockDbObj.Quantity >= balansQty)
                                {
                                    RequestItem.AllocatedQty += balansQty;
                                    inventoryStockDbObj.Quantity -= balansQty;
                                    usedQty = balansQty;
                                }
                                else
                                {
                                    RequestItem.AllocatedQty += inventoryStockDbObj.Quantity;
                                    inventoryStockDbObj.Quantity = 0;
                                    usedQty = inventoryStockDbObj.Quantity;
                                }
                                CoordinatorIntentoryItems.Add(new CoordinatorIntentoryItem
                                {
                                    Date = DateTime.Now,
                                    DateCreated = DateTime.Now,
                                    EffectedQty = usedQty,
                                    InventoryEffectedby = Models.Enums.InventoryEffectedby.Request,
                                    EffectedTransacionID = pendingRequest.ID,
                                    ItemID = item.ItemID,
                                    User = donation.User,
                                    State = Models.Interfaces.State.Added,
                                    AuditReference = item.ItemID.ToString()
                                });
                                RequestItem.State = Models.Interfaces.State.Modified;
                            }
                            var requestStatus = Models.Enums.RequestStatus.IssuancePending;
                            foreach (var requestItem in pendingRequest.RequestItemList)
                            {
                                if (requestItem.RequestedQty != requestItem.AllocatedQty)
                                    requestStatus = Models.Enums.RequestStatus.AllocationPending;
                            }
                            pendingRequest.RequestStatus = requestStatus;
                            pendingRequest.State = Models.Interfaces.State.Modified;
                        }
                    }
                }
            }

            EmailOutBox emailOutBox = null;
            if (donation.DonationSatus == Models.Enums.DonationSatus.Collected)
            {
                emailOutBox = GenerateEmailItem(donation, websettings.FunctionURLs);
            }

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
                if (dbDonation.DonationSatus == DonationSatus.Collected)
                    dbDonation.DateCollected = donation.DateCollected;
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

                    foreach (var coordinatorIntentoryItem in CoordinatorIntentoryItems)
                    {
                        var coordinatorIntentoryItemTO = BizObjectFactory.GetCoordinatorIntentoryItemBO().Save(coordinatorIntentoryItem);
                        if (coordinatorIntentoryItemTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            transferObject.StatusInfo = coordinatorIntentoryItemTO.StatusInfo;
                            return transferObject;
                        }
                    }
                    foreach (var inventoryStock in InventoryStocks)
                    {
                        var inventoryStockTO = BizObjectFactory.GetInventoryStockBO().Save(inventoryStock);
                        if (inventoryStockTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            transferObject.StatusInfo = inventoryStockTO.StatusInfo;
                            return transferObject;
                        }
                    }

                    if (donation.DonationSatus == DonationSatus.Collected)
                    {
                        var emailTO = BizObjectFactory.GetEmailOutBoxBO().Save(emailOutBox);
                        if (emailTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            transferObject.StatusInfo = emailTO.StatusInfo;
                            return transferObject;
                        }
                    }

                    foreach (var pendingRequest in pendingRequests)
                    {
                        var requestTo = BizObjectFactory.GetRequestBO().Save(pendingRequest);
                        if (requestTo.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                        {
                            transferObject.StatusInfo = requestTo.StatusInfo;
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

        public EmailOutBox GenerateEmailItem(Donation donation, FunctionURLs FunctionURLs)
        {
            var emails = string.Empty;
            var emailList = new List<string>();
            var pendingRequests = BizObjectFactory.GetRequestBO().Search(new Models.SearchQueries.RequestSearchQuery { RequestStatus = RequestStatus.AllocationPending });
            foreach (var pendingRequest in pendingRequests)
            {
                var reciption = BizObjectFactory.GetRecipientBO().GetSingle(pendingRequest.RecipientID.Value);
                foreach (var contactPerson in reciption.ContactPersonList)
                {
                    if (!string.IsNullOrEmpty(contactPerson.Email))
                    {
                        if (!emailList.Contains(contactPerson.Email))
                            emailList.Add(contactPerson.Email);
                    }
                }
            }
            emails = string.Join(",", emailList);
            EmailOutBox email = new EmailOutBox();
            email.IsBodyHtml = true;
            email.State = Models.Interfaces.State.Added;
            email.Sender = Models.Enums.EmailSender.General;
            email.To = emails;
            email.Subject = "Welcome to VAMK FWMS";
            email.EmailStatus = EmailStatus.Pending;
            email.EmailSendAttempt = 0;

            EmployeeMessageModel messageModel = new EmployeeMessageModel();
            messageModel.UserName = donation.User;
            messageModel.FunctionURL = FunctionURLs.Login;

            email.MailContent = new EmailItemsFacade(websettings).GenerateEmailcontentFromTemplate<EmployeeMessageModel>(EmailTemplate.DONATION_COLLECTED, messageModel);

            return email;
        }
    }
}
