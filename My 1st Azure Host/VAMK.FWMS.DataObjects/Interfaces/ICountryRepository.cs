using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        /// <summary>
        /// Search all Countries
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Country> Search(CountrySearchQuery query);
    }
}
