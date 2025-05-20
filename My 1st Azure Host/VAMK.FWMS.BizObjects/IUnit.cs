using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IUnit : IBizObjectBase<Unit>
    {
        /// <summary>
        /// Search all Units
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Unit> Search(UnitSearchQuery query);
    }
}
