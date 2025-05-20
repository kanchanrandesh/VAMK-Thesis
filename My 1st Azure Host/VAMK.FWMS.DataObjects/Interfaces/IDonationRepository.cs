using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IDonationRepository : IRepository<Donation>
    {
        /// <summary>
        /// Search all Donations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<Donation> Search(DonationSearchQuery query);
    }
}
