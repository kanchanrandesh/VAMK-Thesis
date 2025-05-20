using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMK.FWMS.Models.Enums;

namespace VAMK.FWMS.WebSite.Models
{
    public class DonationItemModel
    {
        public string @donation { get; set; }
        public string id { get; set; }
        public string donationId { get; set; }
        public string itemId { get; set; }
        public decimal qty { get; set; }
        public string dateCreated { get; set; }
        public string timeStamp { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.DonationItem(DonationItemModel e)
        {
            return new VAMK.FWMS.Models.DonationItem()
            {
                ID = Utility.ParseInt(e.id),
                Qty = e.qty,
                ItemID = Utility.ParseInt(e.itemId),
                DonationID = Utility.ParseInt(e.donationId),
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
            };
        }

        public static explicit operator DonationItemModel(VAMK.FWMS.Models.DonationItem e)
        {
            return new DonationItemModel()
            {
                id = e.ID.Value.ToString(),
                donationId = e.DonationID.Value.ToString(),
                itemId = e.ItemID.Value.ToString(),
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}