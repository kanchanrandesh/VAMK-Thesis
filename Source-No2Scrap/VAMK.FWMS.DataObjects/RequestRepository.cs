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
            {
                DateTime searchday = query.Date.Value.Date.AddDays(1);
                queryble = queryble.Where(t => t.Date == searchday);
            }


            if (!string.IsNullOrEmpty(query.ManualRefNumber))
                queryble = queryble.Where(t => t.ManualRefNumber.Contains(query.ManualRefNumber));
            if (!string.IsNullOrEmpty(query.TransactionNumber))
                queryble = queryble.Where(t => t.TransacionNumber.Contains(query.TransactionNumber));
            if (!string.IsNullOrEmpty(query.RequestStatus.ToString()))
                if ((query.RequestStatus.ToString() != "ALL") && (query.RequestStatus.ToString() != "0"))
                    queryble = queryble.Where(t => (int)t.RequestStatus == (int)query.RequestStatus);

            return queryble.Include(i => i.Recipient).Include(i => i.RequestItemList).OrderByDescending(x => x.Date).ThenByDescending(x => x.TransacionNumber).ToList();
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
