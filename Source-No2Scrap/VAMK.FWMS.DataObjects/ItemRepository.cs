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
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository() : this(new RepositoryContext()) { }

        public ItemRepository(IRepositoryContext context) : base(context) { }

        public IList<Item> Search(Models.SearchQueries.ItemSearchQuery query)
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

            queryble = queryble.Include(t => t.Unit);

            return queryble.OrderBy(t => t.Code).ToList();
        }

    }
}
