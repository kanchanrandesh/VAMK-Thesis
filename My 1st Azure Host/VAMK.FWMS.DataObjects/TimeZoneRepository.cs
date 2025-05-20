using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class TimeZoneRepository : RepositoryBase<TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository() : this(new RepositoryContext()) { }

        public TimeZoneRepository(IRepositoryContext context) : base(context) { }

        public IList<TimeZone> Search(Models.SearchQueries.TimeZoneSearchQuery query)
        {
            if (query == null)
                return null;
            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.Key))
                queryble = queryble.Where(t => t.Key.Contains(query.Key));

            if (!string.IsNullOrEmpty(query.DisplayName))
                queryble = queryble.Where(t => t.DisplayName.Contains(query.DisplayName));

            return queryble.ToList();
        }
    }
}
