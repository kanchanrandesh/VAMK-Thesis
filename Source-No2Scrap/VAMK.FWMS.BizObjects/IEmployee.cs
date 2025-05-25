using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IEmployee : IBizObjectBase<SystemUser>
    {
        /// <summary>
        /// Search all Employees
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<SystemUser> Search(EmployeeSearchQuery query);

        SystemUser ValidateLoginCredential(string username, string password);

        /// <summary>
        /// Get Employee by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        SystemUser GetEmployeeByUserName(string userName);
    }
}
