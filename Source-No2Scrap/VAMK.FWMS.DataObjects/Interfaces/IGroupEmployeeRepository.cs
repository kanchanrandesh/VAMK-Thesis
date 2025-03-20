using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IGroupEmployeeRepository : IRepository<GroupEmployee>
    {
        /// <summary>
        /// Get all Group Employees for Group
        /// </summary>
        /// <param name="eventObj"></param>
        /// <returns></returns>
        IList<GroupEmployee> GetAllFor(Group groupObj);

        IList<GroupEmployee> GetUserGroups(int userId);

        GroupEmployee CheckSalesFullControl(int employeeId);
    }
}
