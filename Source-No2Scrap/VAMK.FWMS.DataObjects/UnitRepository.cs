using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class UnitOfMeasurementRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitOfMeasurementRepository() : this(new RepositoryContext()) { }

        public UnitOfMeasurementRepository(IRepositoryContext context) : base(context) { }

        public IList<Unit> Search(Models.SearchQueries.UnitSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Code.Contains(query.SearchText) || t.UnitName.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Code))
                queryble = queryble.Where(t => t.Code.Contains(query.Code));

            if (!string.IsNullOrEmpty(query.Name))
                queryble = queryble.Where(t => t.UnitName.Contains(query.Name));

            return queryble.OrderBy(t => t.Code).ToList();
        }
    }
}
