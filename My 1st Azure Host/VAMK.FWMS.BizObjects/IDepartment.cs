using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IDepartment : IBizObjectBase<Department>
    {
        /// <summary>
        /// Search all Departments
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Department> Search(DepartmentSearchQuery query);
    }
}
