using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        /// <summary>
        /// Search all Items
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Item> Search(ItemSearchQuery query);
    }
}
