using VAMK.FWMS.Models;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IGroupEmployee : IBizObjectBase<GroupUser>
    {
        /// <summary>
        /// Get all Group Employees for Group
        /// </summary>
        /// <param name="groupObj"></param>
        /// <returns></returns>
        IList<GroupUser> GetAllFor(Group groupObj);

        IList<GroupUser> GetAllFor(int userId);

        List<string> GetRulesForUser(int userId);

        GroupUser CheckSalesFullControl(int employeeId);
    }
}
