using System.Collections.Generic;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IRequestRepository : IRepository<Request>
    {
        /// <summary>
        /// Search all Requests
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Request> Search(RequestSearchQuery query);
    }
}
