using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class RequestItemModel
    {
        public string id { get; set; }

        public string requestID { get; set; }
        public string itemID { get; set; }
        public string requestedQty { get; set; }
        public string allocatedQty { get; set; }
        public bool isFullfilled { get; set; }

        public string dateCreated { get; set; }
        public string timeStamp { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.RequestItem(RequestItemModel e)
        {
            return new VAMK.FWMS.Models.RequestItem()
            {
                ID = Utility.ParseInt(e.id),
                ItemID = Utility.ParseInt(e.itemID),
                RequestID = Utility.ParseInt(e.requestID),
                AllocatedQty = (!string.IsNullOrEmpty(e.requestedQty)?int.Parse(e.requestedQty):0),
                IsFullfilled = e.isFullfilled,
                RequestedQty = (!string.IsNullOrEmpty(e.requestedQty) ? int.Parse(e.requestedQty) : 0) ,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
            };
        }

        public static explicit operator RequestItemModel(VAMK.FWMS.Models.RequestItem e)
        {
            return new RequestItemModel()
            {
                id = e.ID.Value.ToString(),
                requestID = e.RequestID.Value.ToString(),
                allocatedQty = e.AllocatedQty.ToString(),
                requestedQty = e.RequestedQty.ToString(),
                isFullfilled = e.IsFullfilled,
                itemID = e.ItemID.ToString(),
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}