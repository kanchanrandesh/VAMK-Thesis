using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class UnitBO : BizObjectBase<Unit>, IUnit
    {
        #region Vars

        IUnitRepository unitRepository;

        #endregion

        public override Unit Create()
        {
            return new Unit();
        }

        #region Property Fileld

        public IUnitRepository Repository { get { return unitRepository; } }

        #endregion

        #region Costructor

        public UnitBO() : this(new UnitOfMeasurementRepository()) { }

        public UnitBO(IUnitRepository repository)
            : base(repository)
        {
            unitRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Units for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Unit> Search(Models.SearchQueries.UnitSearchQuery query)
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
