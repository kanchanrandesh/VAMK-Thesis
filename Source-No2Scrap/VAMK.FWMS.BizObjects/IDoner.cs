using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface IDoner : IBizObjectBase<Doner>
    {
        /// <summary>
        /// Search all Doners
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Doner> Search(DonerSearchQuery query);
    }
}
