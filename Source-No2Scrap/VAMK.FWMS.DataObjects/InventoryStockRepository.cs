using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class InventoryStockRepository : RepositoryBase<InventoryStock>, IInventoryStockRepository
    {
        public InventoryStockRepository() : this(new RepositoryContext()) { }

        public InventoryStockRepository(IRepositoryContext context) : base(context) { }

        public IList<InventoryStock> Search(Models.SearchQueries.InventoryStockSearchQuery query)
        {
            if (query == null)
                return null;

            var queryble = GetQueryable();
            queryble = queryble.Where(t => t.ItemID == query.ItemId);
            return queryble.ToList();
        }
        public InventoryStock FindItemStock(int ItemId)
        {
            var queryble = GetQueryable();
            queryble = queryble.Where(t => t.ItemID == ItemId);
            return queryble.FirstOrDefault();
        }
    }
}
