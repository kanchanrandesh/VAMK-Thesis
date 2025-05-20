using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository() : this(new RepositoryContext()) { }

        public DepartmentRepository(IRepositoryContext context) : base(context) { }

        public IList<Department> Search(Models.SearchQueries.DepartmentSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Code.Contains(query.SearchText) || t.Name.Contains(query.SearchText) || t.AuthorizedOfficer.FirstName.Contains(query.SearchText) || t.AuthorizedOfficer.LastName.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Code))
                queryble = queryble.Where(t => t.Code.Contains(query.Code));

            if (!string.IsNullOrEmpty(query.Name))
                queryble = queryble.Where(t => t.Name.Contains(query.Name));

            return queryble.OrderBy(t => t.Code).ToList();
        }
    }
}
