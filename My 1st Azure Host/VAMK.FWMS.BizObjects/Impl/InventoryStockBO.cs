using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class InventoryStockBO : BizObjectBase<InventoryStock>, IInventoryStock
    {
        #region Vars

        IInventoryStockRepository inventoryStockRepository;

        #endregion

        public override InventoryStock Create()
        {
            return new InventoryStock();
        }

        #region Property Fileld

        public IInventoryStockRepository Repository { get { return inventoryStockRepository; } }

        #endregion

        #region Costructor

        public InventoryStockBO() : this(new InventoryStockRepository()) { }

        public InventoryStockBO(IInventoryStockRepository repository)
            : base(repository)
        {
            inventoryStockRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Countries for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<InventoryStock> Search(Models.SearchQueries.InventoryStockSearchQuery query)
        {
            return Repository.Search(query);
        }

        /// <summary>
        /// Return Item  for a given item Id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public InventoryStock FindItemStock(int itemId)
        {
            return Repository.FindItemStock(itemId);
        }

        #endregion

        #region Object Type

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #endregion
    }
}
