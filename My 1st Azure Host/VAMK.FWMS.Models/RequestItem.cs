using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class RequestItem : EntityBase
    {

        public Request Request { get; set; }
        public int? RequestID { get; set; }
        public Item Item { get; set; }
        public int? ItemID { get; set; }
        public decimal RequestedQty { get; set; }
        public decimal AllocatedQty { get; set; }
        public bool IsFullfilled { get; set; }
    }
}