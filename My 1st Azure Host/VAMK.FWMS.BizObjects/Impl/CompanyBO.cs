using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class CompanyBO : BizObjectBase<Company>, ICompany
    {
        #region Vars

        ICompanyRepository companyRepository;

        #endregion

        public override Company Create()
        {
            return new Company();
        }

        #region Property Fileld

        public ICompanyRepository Repository { get { return companyRepository; } }

        #endregion

        #region Costructor

        public CompanyBO() : this(new CompanyRepository()) { }

        public CompanyBO(ICompanyRepository repository)
            : base(repository)
        {
            companyRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Companies for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Company> Search(Models.SearchQueries.CompanySearchQuery query)
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
