using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class GroupBO : BizObjectBase<Group>, IGroup
    {
        #region Vars

        IGroupRepository groupRepository;

        #endregion

        public override Group Create()
        {
            return new Group();
        }

        #region Property Fileld

        public IGroupRepository Repository { get { return groupRepository; } }

        #endregion

        #region Costructor

        public GroupBO() : this(new GroupRepository()) { }

        public GroupBO(IGroupRepository repository)
            : base(repository)
        {
            groupRepository = repository;
        }

        #endregion

        #region Search

        /// <summary>
        /// Return list of Groups for a given query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<Group> Search(Models.SearchQueries.GroupSearchQuery query)
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
