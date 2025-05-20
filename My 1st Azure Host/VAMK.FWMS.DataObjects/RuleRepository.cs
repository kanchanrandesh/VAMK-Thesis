using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class RuleRepository : RepositoryBase<Rule>, IRuleRepository
    {
        public RuleRepository() : this(new RepositoryContext()) { }

        public RuleRepository(IRepositoryContext context) : base(context) { }

        public IList<Rule> Search(Models.SearchQueries.RuleSearchQuery query)
        {
            if (query == null)
                return null;
            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Code.Contains(query.SearchText) || t.Description.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Code))
                queryble = queryble.Where(t => t.Code.Contains(query.Code));

            if (!string.IsNullOrEmpty(query.Description))
                queryble = queryble.Where(t => t.Description.Contains(query.Description));

            return queryble.ToList();
        }
    }
}
