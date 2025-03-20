using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository() : this(new RepositoryContext()) { }

        public GroupRepository(IRepositoryContext context) : base(context) { }

        public IList<Group> Search(Models.SearchQueries.GroupSearchQuery query)
        {
            if (query == null)
                return null;
            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Description.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Description))
                queryble = queryble.Where(t => t.Description.Contains(query.Description));

            if (query.IsActive != null)
                queryble = queryble.Where(t => t.IsActive == query.IsActive);

            return queryble.ToList();
        }
    }
}
