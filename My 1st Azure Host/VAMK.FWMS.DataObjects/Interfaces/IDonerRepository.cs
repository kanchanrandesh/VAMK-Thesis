using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IDonerRepository : IRepository<Doner>
    {
        /// <summary>
        /// Search all Doners
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Doner> Search(DonerSearchQuery query);
    }
}
