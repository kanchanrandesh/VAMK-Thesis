using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.DataObjects
{
    public class DonerRepository : RepositoryBase<Doner>, IDonerRepository
    {
        public DonerRepository() : this(new RepositoryContext()) { }

        public DonerRepository(IRepositoryContext context) : base(context) { }

        public IList<Doner> Search(Models.SearchQueries.DonerSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                queryble = queryble.Where(t => t.Code.Contains(query.SearchText) || t.Name.Contains(query.SearchText));

            if (!string.IsNullOrEmpty(query.Code))
                queryble = queryble.Where(t => t.Code.Contains(query.Code));

            if (!string.IsNullOrEmpty(query.Name))
                queryble = queryble.Where(t => t.Name.Contains(query.Name));

            return queryble.OrderBy(t => t.Code).ToList();
        }

        public override Doner GetSingle(System.Linq.Expressions.Expression<System.Func<Doner, bool>> whereCondition)
        {
            var returnVal = this.DbSet.Where(whereCondition)
                .FirstOrDefault();

            returnVal.ContactPersonList = new ContactPersonRepository().GetAllFor(returnVal);

            return returnVal;
        }
    }
}
