using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IRecipientRepository : IRepository<Recipient>
    {
        /// <summary> 
        /// Search all Recipient
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Recipient> Search(RecipientSearchQuery query);
    }
}
