using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface IItem : IBizObjectBase<Item>
    {
        /// <summary>
        /// Search all Items
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Item> Search(ItemSearchQuery query);
    }
}
