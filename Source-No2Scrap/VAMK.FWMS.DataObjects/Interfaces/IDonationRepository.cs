using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IDonationRepository : IRepository<Donation>
    {
        /// <summary>
        /// Get All Donations  
        /// </summary>        
        //Donation GetAllDonations();
    }
}
