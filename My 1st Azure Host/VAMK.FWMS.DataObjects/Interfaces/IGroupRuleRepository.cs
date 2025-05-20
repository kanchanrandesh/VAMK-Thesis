using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IGroupRuleRepository : IRepository<GroupRule>
    {
        /// <summary>
        /// Get all Group Rules for Group
        /// </summary>
        /// <param name="eventObj"></param>
        /// <returns></returns>
        IList<GroupRule> GetAllFor(Group groupObj);

        IList<GroupRule> GetAllFor(List<int> groupIds);
    }
}
