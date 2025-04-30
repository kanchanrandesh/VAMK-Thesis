using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class CoordinatorIntentoryItemBO : BizObjectBase<CoordinatorIntentoryItem>, ICoordinatorIntentoryItem
    {
        #region Vars

        ICoordinatorIntentoryItemRepository coordinatorIntentoryItemRepository;

        #endregion

        public override CoordinatorIntentoryItem Create()
        {
            return new CoordinatorIntentoryItem();
        }

        #region Property Fileld

        public ICoordinatorIntentoryItemRepository Repository { get { return coordinatorIntentoryItemRepository; } }

        #endregion

        #region Costructor

        public CoordinatorIntentoryItemBO() : this(new CoordinatorIntentoryItemRepository()) { }

        public CoordinatorIntentoryItemBO(ICoordinatorIntentoryItemRepository repository)
            : base(repository)
        {
            coordinatorIntentoryItemRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Countries for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<CoordinatorIntentoryItem> Search(Models.SearchQueries.CoordinatorIntentoryItemSearchQuery query)
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
