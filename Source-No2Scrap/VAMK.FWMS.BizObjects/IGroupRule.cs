using VAMK.FWMS.Models;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IGroupRule : IBizObjectBase<GroupRule>
    {
        /// <summary>
        /// Get all Group Rules for Group
        /// </summary>
        /// <param name="groupObj"></param>
        /// <returns></returns>
        IList<GroupRule> GetAllFor(Group groupObj);
    }
}
