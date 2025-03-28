using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface IRecipient : IBizObjectBase<Recipient>
    {
        /// <summary>
        /// Search all Recipients
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Recipient> Search(RecipientSearchQuery query);
    }
}
