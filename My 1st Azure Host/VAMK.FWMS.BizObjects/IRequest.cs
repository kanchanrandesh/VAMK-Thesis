using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface IRequest : IBizObjectBase<Request>
    {
        /// <summary>
        /// Search all Requests
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Request> Search(RequestSearchQuery query);
    }
}
