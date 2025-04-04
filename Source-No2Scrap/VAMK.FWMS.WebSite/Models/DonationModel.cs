using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class DonationModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string dateCreated { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string transacionNumber { get; set; }
        public string manualRefNumber { get; set; }
        public string donationSatus { get; set; }
        public string displayDonationSatus { get; set; }

        public string donerID { get; set; }
        public string donerName { get; set; }
        public List<DonationItemModel> donationItemList { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Donation(DonationModel e)
        {
            var _donationItemList = new List<VAMK.FWMS.Models.DonationItem>();
            if (e.donationItemList != null && e.donationItemList.Count > 0)
            {
                foreach (var listItem in e.donationItemList)
                {
                    _donationItemList.Add(new VAMK.FWMS.Models.DonationItem()
                    {
                        DonationID = Utility.ParseInt(listItem.donationId),
                        ItemID = !String.IsNullOrWhiteSpace(listItem.itemId) ? int.Parse(listItem.itemId) : 0,
                        Qty = listItem.qty,
                        TimeStamp = listItem.id != null ? Utility.StringToTimeStamp(listItem.timeStamp) : new byte[8],
                        ID = Utility.ParseInt(listItem.id),
                    });
                }
            }

            return new VAMK.FWMS.Models.Donation()
            {
                ID = Utility.ParseInt(e.id),
                Date = Convert.ToDateTime(e.date),
                TransacionNumber = e.transacionNumber,
                ManualRefNumber = e.manualRefNumber,
                Description = e.description,
                DonationSatus = e.id != null ? (VAMK.FWMS.Models.Enums.DonationSatus)Enum.Parse(typeof(VAMK.FWMS.Models.Enums.DonationSatus), e.donationSatus) : VAMK.FWMS.Models.Enums.DonationSatus.ReadyToPickup,
                DonerID = Utility.ParseInt(e.donerID),
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
                DonationItemList = _donationItemList
            };


        }

        public static explicit operator DonationModel(VAMK.FWMS.Models.Donation e)
        {
            //Contact Person List
            var donationItemList = new List<VAMK.FWMS.WebSite.Models.DonationItemModel>();
            if (e.DonationItemList != null && e.DonationItemList.Count > 0)
            {
                foreach (var listItem in e.DonationItemList)
                {
                    donationItemList.Add(new DonationItemModel()
                    {
                        id = listItem.ID.ToString(),
                        itemId = listItem.ItemID.ToString(),
                        qty = listItem.Qty,
                        timeStamp = Utility.TimeStampToString(listItem.TimeStamp),

                    });
                }
            }

            return new DonationModel()
            {
                id = e.ID.Value.ToString(),
                donerID = e.DonerID.ToString(),
                donerName = e.DonerID != null ? e.Doner.Name : string.Empty,
                description = e.Description,
                date = e.Date.ToShortDateString(),
                manualRefNumber = e.ManualRefNumber,
                transacionNumber = e.TransacionNumber,
                donationSatus = e.DonationSatus.ToString(),
                displayDonationSatus = Regex.Replace(e.DonationSatus.ToString(), "(?<!^)([A-Z])", " $1"),
                timeStamp = Utility.TimeStampToString(e.TimeStamp),
                donationItemList = donationItemList,
            };
        }
    }
}
