using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class CoordinatorIntentoryItemRepository : RepositoryBase<CoordinatorIntentoryItem>, ICoordinatorIntentoryItemRepository
    {
        public CoordinatorIntentoryItemRepository() : this(new RepositoryContext()) { }

        public CoordinatorIntentoryItemRepository(IRepositoryContext context) : base(context) { }

        public IList<CoordinatorIntentoryItem> Search(Models.SearchQueries.CoordinatorIntentoryItemSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();

            if (!string.IsNullOrEmpty(query.InventoryEffectedby.ToString()))
                queryble = queryble.Where(t => t.InventoryEffectedby == query.InventoryEffectedby);

            if (query.ItemId != null)
                queryble = queryble.Where(t => t.ItemID == query.ItemId);

            return queryble.ToList();
        }
    }
}
