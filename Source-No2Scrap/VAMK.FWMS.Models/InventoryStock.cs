using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class InventoryStock : EntityBase
    {
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

        public decimal Quantity { get; set; } = 0;

    }
}
