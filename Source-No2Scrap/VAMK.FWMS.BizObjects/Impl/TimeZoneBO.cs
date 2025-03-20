using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class TimeZoneBO : BizObjectBase<Models.TimeZone>, ITimeZone
    {
        #region Vars

        ITimeZoneRepository timeZoneRepository;

        #endregion

        public override Models.TimeZone Create()
        {
            return new Models.TimeZone();
        }

        #region Property Fileld

        public ITimeZoneRepository Repository { get { return timeZoneRepository; } }

        #endregion

        #region Costructor

        public TimeZoneBO() : this(new TimeZoneRepository()) { }

        public TimeZoneBO(ITimeZoneRepository repository)
            : base(repository)
        {
            timeZoneRepository = repository;
        }

        #endregion

        #region Get All For

        /// <summary>
        /// Return list of Time Zones for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Models.TimeZone> Search(Models.SearchQueries.TimeZoneSearchQuery query)
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
