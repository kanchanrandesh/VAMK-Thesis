using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class Request : EntityBase
    {

        public Recipient Recipient { get; set; }
        public int? RecipientD { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Models.Enums.RequestStatus RequestStatus { get; set; }
        public IList<RequestItem> RequestItemList { get; set; }
    }
}