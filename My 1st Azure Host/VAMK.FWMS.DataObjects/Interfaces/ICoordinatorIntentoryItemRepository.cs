using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface ICoordinatorIntentoryItemRepository : IRepository<CoordinatorIntentoryItem>
    {
        /// <summary>
        /// Search all Coordinator Intentory Item
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<CoordinatorIntentoryItem> Search(CoordinatorIntentoryItemSearchQuery query);
    }
}
