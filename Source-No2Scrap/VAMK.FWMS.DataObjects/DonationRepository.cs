using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;

namespace VAMK.FWMS.DataObjects
{
   
    public class DonationRepository : RepositoryBase<Donation>, IDonationRepository
    {
        public DonationRepository() : this(new RepositoryContext()) { }

        public DonationRepository(IRepositoryContext context) : base(context) { }

        public IList<Donation> GetAllDonations()
        {
            return DbSet.ToList();
        }

    }
}
