using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IGroupEmployeeRepository : IRepository<GroupUser>
    {
        /// <summary>
        /// Get all Group Employees for Group
        /// </summary>
        /// <param name="eventObj"></param>
        /// <returns></returns>
        IList<GroupUser> GetAllFor(Group groupObj);

        IList<GroupUser> GetUserGroups(int userId);

        GroupUser CheckSalesFullControl(int employeeId);
    }
}
