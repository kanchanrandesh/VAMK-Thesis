using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface ICompany : IBizObjectBase<Company>
    {
        /// <summary>
        /// Search all Companies
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Company> Search(CompanySearchQuery query);        
    }
}
