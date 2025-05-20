using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// Search all Companies
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Company> Search(CompanySearchQuery query);    
      
    }
}
