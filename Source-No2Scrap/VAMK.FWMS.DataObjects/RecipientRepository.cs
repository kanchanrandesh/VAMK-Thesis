using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects
{
    public class RecipientRepository : RepositoryBase<Recipient>, IRecipientRepository
    {
        
            public RecipientRepository() : this(new RepositoryContext()) { }

            public RecipientRepository(IRepositoryContext context) : base(context) { }

            public IList<Recipient> Search(Models.SearchQueries.RecipientSearchQuery query)
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
        public override Recipient GetSingle(System.Linq.Expressions.Expression<System.Func<Recipient, bool>> whereCondition)
        {
            var returnVal = this.DbSet.Where(whereCondition)
                .FirstOrDefault();

            returnVal.ContactPersonList = new ContactPersonRepository().GetAllFor(returnVal);

            return returnVal;
        }
    }
}
