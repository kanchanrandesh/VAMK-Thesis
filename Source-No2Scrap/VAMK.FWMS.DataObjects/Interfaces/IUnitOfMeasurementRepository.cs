using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IUnitOfMeasurementRepository : IRepository<UnitOfMeasurement>
    {
        /// <summary>
        /// Search all Units
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<UnitOfMeasurement> Search(UnitSearchQuery query);
    }
}
