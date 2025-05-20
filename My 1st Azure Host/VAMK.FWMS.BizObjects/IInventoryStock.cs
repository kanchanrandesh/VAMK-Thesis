using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.BizObjects
{
    public interface IInventoryStock : IBizObjectBase<InventoryStock>
    {
        /// <summary>
        /// Search all Inventory Stock
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<InventoryStock> Search(InventoryStockSearchQuery query);

        /// <summary>
        ///  Return Item  for a given item Id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        InventoryStock FindItemStock(int itemId);
    }
}
