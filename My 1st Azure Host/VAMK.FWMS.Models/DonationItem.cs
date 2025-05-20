using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class DonationItem : EntityBase
    {
        public Donation Donation { get; set; }        
        public int? DonationID { get; set; }
        public Item Item { get; set; }        
        public int ? ItemID { get; set; }        
        public decimal Qty { get; set; }
               
        private string _productCode;
        public string ProductCode //For Audit Reference only
        {
            get { return _productCode; }
            set
            {
                _productCode = value;
                AuditReference = value;
            }
        }
    }
}