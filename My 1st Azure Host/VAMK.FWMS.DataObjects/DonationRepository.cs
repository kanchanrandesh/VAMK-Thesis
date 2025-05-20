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

    public class DonationRepository : RepositoryBase<Donation>, IDonationRepository
    {
        public DonationRepository() : this(new RepositoryContext()) { }

        public DonationRepository(IRepositoryContext context) : base(context) { }

        public IList<Donation> Search(Models.SearchQueries.DonationSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();


            if (!string.IsNullOrEmpty(query.ManualRefNumber))
                queryble = queryble.Where(t => t.ManualRefNumber.Contains(query.ManualRefNumber));
            if (!string.IsNullOrEmpty(query.TransacionNumber))
                queryble = queryble.Where(t => t.TransacionNumber.Contains(query.TransacionNumber));


            string dateString = query.Date.ToString();

            // Remove the curly braces if they exist
            dateString = dateString.Trim('{', '}');

            // Try to parse it
            if (query.Date != null)
            {
                DateTime searchday = query.Date.Value.Date.AddDays(1);
                queryble = queryble.Where(t => t.Date == searchday);
            }


            if (!string.IsNullOrEmpty(query.DonationSatus.ToString()))
                if ((query.DonationSatus.ToString() != "ALL") && (query.DonationSatus.ToString() != "0"))
                    queryble = queryble.Where(t => (int)t.DonationSatus == (int)query.DonationSatus);

            return queryble.Include(i => i.Doner).OrderByDescending(x => x.Date).ThenByDescending(x => x.TransacionNumber).ToList();
        }

        public override Donation GetSingle(System.Linq.Expressions.Expression<System.Func<Donation, bool>> whereCondition)
        {
            var returnVal = this.DbSet.Where(whereCondition).Include(i => i.Doner)
                .FirstOrDefault();

            returnVal.DonationItemList = new DonationItemRepository().GetAllFor(returnVal);

            return returnVal;
        }
    }
}
