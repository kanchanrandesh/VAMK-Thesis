using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IInventoryStockRepository : IRepository<InventoryStock>
    {
        /// <summary>
        /// Search all Inventory Stock
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<InventoryStock> Search(InventoryStockSearchQuery query);

        /// <summary>
        /// Find Item Inventory Stock
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        InventoryStock FindItemStock(int ItemId);
    }
}
