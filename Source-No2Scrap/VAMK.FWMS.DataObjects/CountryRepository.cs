using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository() : this(new RepositoryContext()) { }

        public CountryRepository(IRepositoryContext context) : base(context) { }

        public IList<Country> Search(Models.SearchQueries.CountrySearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Name.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Name))
                queryble = queryble.Where(t => t.Name.Contains(query.Name));

            return queryble.OrderBy(t => t.Name).ToList();
        }
    }
}
