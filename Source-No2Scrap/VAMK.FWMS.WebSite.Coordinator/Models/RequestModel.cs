using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VAMK.FWMS.Models;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class RequestModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string dateAccepted { get; set; }
        public string dateIssued { get; set; }
        public string recipientId { get; set; }
        public string recipientName { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string transacionNumber { get; set; }
        public string manualRefNumber { get; set; }
        public string requestStatus { get; set; }
        public List<RequestItemModel> requestItemList { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Request(RequestModel e)
        {
            var _requestItemList = new List<VAMK.FWMS.Models.RequestItem>();
            if (e.requestItemList != null && e.requestItemList.Count > 0)
            {
                foreach (var listItem in e.requestItemList)
                {
                    _requestItemList.Add(new VAMK.FWMS.Models.RequestItem()
                    {
                        RequestID = Utility.ParseInt(listItem.requestID),
                        ItemID = !String.IsNullOrWhiteSpace(listItem.itemID) ? int.Parse(listItem.itemID) : 0,
                        IsFullfilled = listItem.isFullfilled,
                        RequestedQty = (!string.IsNullOrEmpty(listItem.requestedQty) ? decimal.Parse(listItem.requestedQty) : 0),
                        AllocatedQty = (!string.IsNullOrEmpty(listItem.allocatedQty) ? decimal.Parse(listItem.allocatedQty) : 0),
                        TimeStamp = listItem.id != null ? Utility.StringToTimeStamp(listItem.timeStamp) : new byte[8],
                        ID = Utility.ParseInt(listItem.id),
                    });
                }
            }

            return new VAMK.FWMS.Models.Request()
            {
                ID = Utility.ParseInt(e.id),
                Date = Convert.ToDateTime(e.date),
                TransacionNumber = e.transacionNumber,
                ManualRefNumber = e.manualRefNumber,
                Description = e.description,
                RecipientID = Utility.ParseInt(e.recipientId),
                RequestStatus = (e.id == null ? VAMK.FWMS.Models.Enums.RequestStatus.AllocationPending : (VAMK.FWMS.Models.Enums.RequestStatus)Enum.Parse(typeof(VAMK.FWMS.Models.Enums.RequestStatus), e.requestStatus)),
                RequestItemList = _requestItemList,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
                DateAccepted = Convert.ToDateTime(e.dateAccepted),
                DateIssued = Convert.ToDateTime(e.dateIssued),
            };


        }

        public static explicit operator RequestModel(VAMK.FWMS.Models.Request e)
        {
            //Contact Person List
            var requestItemList = new List<VAMK.FWMS.WebSite.Models.RequestItemModel>();
            if (e.RequestItemList != null && e.RequestItemList.Count > 0)
            {
                foreach (var listItem in e.RequestItemList)
                {
                    requestItemList.Add(new RequestItemModel()
                    {
                        id = listItem.ID.ToString(),
                        allocatedQty = listItem.AllocatedQty.ToString(),
                        isFullfilled = listItem.IsFullfilled,
                        itemID = listItem.ItemID.ToString(),
                        requestedQty = listItem.RequestedQty.ToString(),
                        requestID = listItem.RequestID.ToString(),
                        timeStamp = Utility.TimeStampToString(listItem.TimeStamp),

                    });
                }
            }

            return new RequestModel()
            {
                id = e.ID.Value.ToString(),
                description = e.Description,
                date = e.Date.ToShortDateString(),
                manualRefNumber = e.ManualRefNumber,
                transacionNumber = e.TransacionNumber,
                recipientId = e.RecipientID.ToString(),
                requestStatus = e.RequestStatus.ToString(),
                recipientName = (e.Recipient != null ? e.Recipient.Name : string.Empty),

                timeStamp = Utility.TimeStampToString(e.TimeStamp),
                requestItemList = requestItemList,
            };
        }
    }
}
