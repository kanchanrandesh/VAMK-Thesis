using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class ItemBO : BizObjectBase<Item>, IItem
    {
        #region Vars

        IItemRepository donerRepository;

        #endregion

        public override Item Create()
        {
            return new Item();
        }

        #region Property Fileld

        public IItemRepository Repository { get { return donerRepository; } }

        #endregion

        #region Costructor

        public ItemBO() : this(new ItemRepository()) { }

        public ItemBO(IItemRepository repository)
            : base(repository)
        {
            donerRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Item for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Item> Search(Models.SearchQueries.ItemSearchQuery query)
        {
            return Repository.Search(query);
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
