using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class CoordinatorIntentoryItem : EntityBase
    {
        public DateTime Date { get; set; }
        public VAMK.FWMS.Models.Enums.InventoryEffectedby InventoryEffectedby { get; set; }
        public int? EffectedTransacionID { get; set; }
        public Item Item { get; set; }

        private int? _itemID;
        public int? ItemID
        {
            get { return _itemID; }
            set
            {
                _itemID = value;
                AuditReference = value != null ? value.Value.ToString() : string.Empty;
            }
        }
        public decimal? EffectedQty { get; set; }
    }
}