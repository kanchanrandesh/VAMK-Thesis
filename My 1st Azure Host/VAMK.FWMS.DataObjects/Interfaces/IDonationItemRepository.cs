using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IDonationItemRepository : IRepository<DonationItem>
    {
        /// <summary>
        /// Get all  for export
        /// </summary>
        /// <returns></returns>
        IList<DonationItem> GetAllForExport();

        /// <summary>
        /// Get all DonationItem for Donation 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        IList<DonationItem> GetAllFor(Donation donation);
    }
}
