using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class UnitBO : BizObjectBase<UnitOfMeasurement>, IUnitOfMeasurement
    {
        #region Vars

        IUnitOfMeasurementRepository unitRepository;

        #endregion

        public override UnitOfMeasurement Create()
        {
            return new UnitOfMeasurement();
        }

        #region Property Fileld

        public IUnitOfMeasurementRepository Repository { get { return unitRepository; } }

        #endregion

        #region Costructor

        public UnitBO() : this(new UnitOfMeasurementRepository()) { }

        public UnitBO(IUnitOfMeasurementRepository repository)
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
        public IList<UnitOfMeasurement> Search(Models.SearchQueries.UnitSearchQuery query)
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
