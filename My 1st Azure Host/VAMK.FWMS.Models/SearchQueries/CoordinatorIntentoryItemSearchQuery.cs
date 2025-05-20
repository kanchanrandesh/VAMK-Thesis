using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.SearchQueries
{
    public class CoordinatorIntentoryItemSearchQuery
    {
        public Enums.InventoryEffectedby? InventoryEffectedby { get; set; }
        public int? ItemId { get; set; }
    }
}
