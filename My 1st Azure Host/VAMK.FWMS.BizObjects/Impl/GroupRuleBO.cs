using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class GroupRuleBO : BizObjectBase<GroupRule>, IGroupRule
    {
        #region Vars

        IGroupRuleRepository groupRuleRepository;

        #endregion

        public override GroupRule Create()
        {
            return new GroupRule();
        }

        #region Property Fileld

        public IGroupRuleRepository Repository { get { return groupRuleRepository; } }

        #endregion

        #region Costructor

        public GroupRuleBO() : this(new GroupRuleRepository()) { }

        public GroupRuleBO(IGroupRuleRepository repository)
            : base(repository)
        {
            groupRuleRepository = repository;
        }

        #endregion

        #region Get All For

        /// <summary>
        /// Return list of Group Rules for a Group
        /// </summary>
        /// <param name="groupObj"></param>
        /// <returns></returns>
        public IList<GroupRule> GetAllFor(Group groupObj)
        {
            return Repository.GetAllFor(groupObj);
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
