using VAMK.FWMS.Models;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IGroupEmployee : IBizObjectBase<GroupEmployee>
    {
        /// <summary>
        /// Get all Group Employees for Group
        /// </summary>
        /// <param name="groupObj"></param>
        /// <returns></returns>
        IList<GroupEmployee> GetAllFor(Group groupObj);

        IList<GroupEmployee> GetAllFor(int userId);

        List<string> GetRulesForUser(int userId);

        GroupEmployee CheckSalesFullControl(int employeeId);
    }
}
