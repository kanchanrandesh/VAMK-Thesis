using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface ITimeZone : IBizObjectBase<TimeZone>
    {
        /// <summary>
        /// Search all Time Zones
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<TimeZone> Search(TimeZoneSearchQuery query);
    }
}
