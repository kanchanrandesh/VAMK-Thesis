using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class DonationItemRepository : RepositoryBase<DonationItem>, IDonationItemRepository
    {
        public DonationItemRepository() : this(new RepositoryContext()) { }

        public DonationItemRepository(IRepositoryContext context) : base(context) { }

        public System.Collections.Generic.IList<DonationItem> GetAllFor(Donation donation)
        {
            return DbSet.Where(t => t.DonationID == donation.ID).ToList();
        }

        public IList<DonationItem> GetAllForExport()
        {
            var returnVal = DbSet.Where(t => t.ID != null)
                .OrderBy(t => t.ID)
                .ThenBy(t => t.DateCreated)
                .ToList();

            return returnVal;
        }
    }
}
