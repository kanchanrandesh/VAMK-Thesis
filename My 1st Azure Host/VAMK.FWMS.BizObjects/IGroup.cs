using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IGroup : IBizObjectBase<Group>
    {
        /// <summary>
        /// Search all Groups
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Group> Search(GroupSearchQuery query);
    }
}
