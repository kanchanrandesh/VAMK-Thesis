using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IRule : IBizObjectBase<Rule>
    {
        /// <summary>
        /// Search all Rules
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Rule> Search(RuleSearchQuery query);
    }
}
