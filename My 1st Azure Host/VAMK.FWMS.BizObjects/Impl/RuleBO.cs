using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class RuleBO : BizObjectBase<Rule>, IRule
    {
        #region Vars

        IRuleRepository ruleRepository;

        #endregion

        public override Rule Create()
        {
            return new Rule();
        }

        #region Property Fileld

        public IRuleRepository Repository { get { return ruleRepository; } }

        #endregion

        #region Costructor

        public RuleBO() : this(new RuleRepository()) { }

        public RuleBO(IRuleRepository repository)
            : base(repository)
        {
            ruleRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Rules for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Rule> Search(Models.SearchQueries.RuleSearchQuery query)
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
