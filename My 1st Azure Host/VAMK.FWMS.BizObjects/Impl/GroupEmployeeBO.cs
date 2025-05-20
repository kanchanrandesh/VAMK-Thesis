using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class GroupEmployeeBO : BizObjectBase<GroupUser>, IGroupEmployee
    {
        #region Vars

        IGroupEmployeeRepository groupEmployeeRepository;

        #endregion

        public override GroupUser Create()
        {
            return new GroupUser();
        }

        #region Property Fileld

        public IGroupEmployeeRepository Repository { get { return groupEmployeeRepository; } }

        #endregion

        #region Costructor

        public GroupEmployeeBO() : this(new GroupEmployeeRepository()) { }

        public GroupEmployeeBO(IGroupEmployeeRepository repository)
            : base(repository)
        {
            groupEmployeeRepository = repository;
        }

        #endregion

        #region Get All For

        /// <summary>
        /// Return list of Group Employees for a Group
        /// </summary>
        /// <param name="groupObj"></param>
        /// <returns></returns>
        public IList<GroupUser> GetAllFor(Group groupObj)
        {
            return Repository.GetAllFor(groupObj);
        }

        public IList<GroupUser> GetAllFor(int userId)
        {
            return Repository.GetUserGroups(userId);
        }

        public List<string> GetRulesForUser(int userId)
        {
            var repository = new GroupRuleRepository();
            var roles = GetAllFor(userId).Select(t => t.Group.ID.Value);
            return repository.GetAllFor(roles.ToList<int>()).Select(t => t.Rule.Code).ToList();
        }

        public GroupUser CheckSalesFullControl(int employeeId)
        {
            return Repository.CheckSalesFullControl(employeeId);
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
