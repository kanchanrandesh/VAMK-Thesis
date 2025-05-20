using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Search all Employees
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Employee> Search(EmployeeSearchQuery query);

        /// <summary>
        /// Get Employee by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Employee GetEmployeeByUserName(string userName);
    }
}
