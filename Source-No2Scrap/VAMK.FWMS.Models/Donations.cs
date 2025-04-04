using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class Donation : EntityBase
    {
        public Doner Doner { get; set; }
        public int? DonerID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string TransacionNumber { get; set; }
        //public string ManualRefNumber { get; set; }
        public Models.Enums.DonationSatus DonationSatus { get; set; }
        public IList<DonationItem> DonationItemList { get; set; }

        private string _manualRefNumber;
        public string ManualRefNumber
        {
            get { return _manualRefNumber; }
            set
            {
                _manualRefNumber = value;
                AuditReference = value;
            }
        }

    }
}