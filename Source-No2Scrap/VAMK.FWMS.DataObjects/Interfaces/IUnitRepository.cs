using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        /// <summary>
        /// Search all Units
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Unit> Search(UnitSearchQuery query);
    }
}
