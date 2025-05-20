using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class CountryBO : BizObjectBase<Country>, ICountry
    {
        #region Vars

        ICountryRepository countryRepository;

        #endregion

        public override Country Create()
        {
            return new Country();
        }

        #region Property Fileld

        public ICountryRepository Repository { get { return countryRepository; } }

        #endregion

        #region Costructor

        public CountryBO() : this(new CountryRepository()) { }

        public CountryBO(ICountryRepository repository)
            : base(repository)
        {
            countryRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Countries for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Country> Search(Models.SearchQueries.CountrySearchQuery query)
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
