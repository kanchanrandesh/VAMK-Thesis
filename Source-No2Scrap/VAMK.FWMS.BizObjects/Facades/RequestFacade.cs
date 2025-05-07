using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static iTextSharp.text.pdf.AcroFields;
using VAMK.FWMS.BizObjects.Impl;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class RequestFacade
    {
        public TransferObject<Request> Save(Request request, SequenceNumber sequenceNumber)
        {
            var CoordinatorIntentoryItems = new List<CoordinatorIntentoryItem>();
            var InventoryStocks = new List<InventoryStock>();
            if (request.RequestStatus == Models.Enums.RequestStatus.Completed)
            {
                foreach (var item in request.RequestItemList)
                {
                    CoordinatorIntentoryItems.Add(new CoordinatorIntentoryItem
                    {
                        Date = DateTime.Now,
                        DateCreated = DateTime.Now,
                        EffectedQty = item.AllocatedQty,
                        InventoryEffectedby = Models.Enums.InventoryEffectedby.Donation,
                        EffectedTransacionID = request.ID,
                        ItemID = item.ItemID,
                        User = request.User,
                        State = Models.Interfaces.State.Added,
                        AuditReference = item.ItemID.ToString()
                    });
                    var inventoryStockDbObj = BizObjectFactory.GetInventoryStockBO().FindItemStock(item.ItemID.Value);
                    if (inventoryStockDbObj == null)
                    {
                        InventoryStocks.Add(new InventoryStock
                        {
                            ItemID = item.ItemID,
                            Quantity = item.AllocatedQty,
                            State = Models.Interfaces.State.Added,
                            User = request.User,
                            DateCreated = DateTime.Now,
                        });
                    }
                    else
                    {
                        inventoryStockDbObj.State = Models.Interfaces.State.Modified;
                        inventoryStockDbObj.Quantity -= item.AllocatedQty;
                        inventoryStockDbObj.User = request.User;
                        inventoryStockDbObj.DateModified = DateTime.Now;
                        InventoryStocks.Add(inventoryStockDbObj);
                    }
                }
            }

            var transferObject = new TransferObject<Request>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            if (request.ID == null)
            {
                request.State = Models.Interfaces.State.Added;

                foreach (var item in request.RequestItemList)
                {
                    item.State = Models.Interfaces.State.Added;
                    if (item.RequestedQty == item.AllocatedQty)
                        item.IsFullfilled = true;
                }
                var auditTrail = Resources.Utility.CreateAuditTrail(null, request, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetRequestBO().Save(request);
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
                var dbRequest = BizObjectFactory.GetRequestBO().GetSingle(request.ID.Value);

                var childListNames = new List<string>();
                childListNames.Add("RequestItemList");

                var auditTrail = Resources.Utility.CreateAuditTrail(dbRequest, request, Models.Enums.AuditTrailAction.Update, childListNames, 0);

                dbRequest.Date = request.Date;
                dbRequest.TransacionNumber = request.TransacionNumber;
                dbRequest.ManualRefNumber = request.ManualRefNumber;
                dbRequest.Description = request.Description;
                dbRequest.RecipientID = request.RecipientID;
                dbRequest.RequestStatus = request.RequestStatus;
                if (dbRequest.RequestStatus == Models.Enums.RequestStatus.IssuancePending)
                    dbRequest.DateAccepted = request.DateAccepted;
                if (dbRequest.RequestStatus == Models.Enums.RequestStatus.Completed)
                    dbRequest.DateIssued = request.DateIssued;
                dbRequest.User = request.User;
                dbRequest.State = Models.Interfaces.State.Modified;

                foreach (var listItem in request.RequestItemList)
                {
                    var dbRequestItem = dbRequest.RequestItemList.Where(i => i.ID == listItem.ID && listItem.ID != null).FirstOrDefault();
                    if (dbRequestItem != null)
                    {
                        dbRequestItem.Request = null;
                        dbRequestItem.RequestID = listItem.RequestID;
                        dbRequestItem.Item = null;
                        dbRequestItem.ItemID = listItem.ItemID;
                        dbRequestItem.User = listItem.User;
                        dbRequestItem.State = Models.Interfaces.State.Modified;
                        dbRequestItem.RequestedQty = listItem.RequestedQty;
                        dbRequestItem.AllocatedQty = listItem.AllocatedQty;
                        dbRequestItem.IsFullfilled = listItem.IsFullfilled;
                    }
                    else
                    {
                        dbRequest.RequestItemList.Add(new RequestItem()
                        {
                            RequestID = listItem.RequestID,
                            ItemID = listItem.ItemID,
                            User = listItem.User,
                            State = Models.Interfaces.State.Added,
                            IsFullfilled = listItem.IsFullfilled,
                            AllocatedQty = listItem.AllocatedQty,
                            RequestedQty = listItem.RequestedQty,
                        });
                    }
                    if (listItem.RequestedQty == listItem.AllocatedQty)
                        listItem.IsFullfilled = true;
                }

                var deletedList = dbRequest.RequestItemList.Where(p => request.RequestItemList.All(p2 => p2.ID != p.ID && p.ID != null));
                foreach (var item in deletedList)
                    item.State = Models.Interfaces.State.Deleted;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    transferObject = BizObjectFactory.GetRequestBO().Save(dbRequest);
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

        public TransferObject<bool> Delete(Request request)
        {
            var transferObject = new TransferObject<bool>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            var auditTrail = Resources.Utility.CreateAuditTrail(null, request, Models.Enums.AuditTrailAction.Delete, new List<string>(), 0);

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetRequestBO().Delete(request.ID.Value);
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
