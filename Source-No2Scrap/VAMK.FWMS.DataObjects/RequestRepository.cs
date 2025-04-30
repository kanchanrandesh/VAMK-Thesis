using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VAMK.FWMS.DataObjects
{

    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository() : this(new RepositoryContext()) { }

        public RequestRepository(IRepositoryContext context) : base(context) { }

        public IList<Request> Search(Models.SearchQueries.RequestSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (query.Date != null)
                queryble = queryble.Where(t => t.Date == query.Date);
            if (!string.IsNullOrEmpty(query.RequestStatus))
                queryble = queryble.Where(t => t.RequestStatus.ToString() ==  query.RequestStatus);

            return queryble.Include(i => i.Recipient).ToList();
        }

        public override Request GetSingle(System.Linq.Expressions.Expression<System.Func<Request, bool>> whereCondition)
        {
            var returnVal = this.DbSet
                .Where(whereCondition)
                .Include(i => i.RequestItemList)
                .FirstOrDefault();
            return returnVal;
        }
    }
}
