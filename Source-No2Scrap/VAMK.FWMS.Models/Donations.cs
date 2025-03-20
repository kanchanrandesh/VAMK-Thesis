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
        public Models.Enums.DonationSatus DonationSatus { get; set; }
        public IList<DonationItem> DonationItemList { get; set; }
    }
}