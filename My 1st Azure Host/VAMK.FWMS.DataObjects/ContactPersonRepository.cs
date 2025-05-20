using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class ContactPersonRepository : RepositoryBase<ContactPerson>, IContactPersonRepository
    {
        public ContactPersonRepository() : this(new RepositoryContext()) { }

        public ContactPersonRepository(IRepositoryContext context) : base(context) { }

        public System.Collections.Generic.IList<ContactPerson> GetAllFor(Doner doner)
        {
            return DbSet.Where(t => t.DonerID == doner.ID && t.IsDoner == true).ToList();
        }

        public System.Collections.Generic.IList<ContactPerson> GetAllFor(Recipient recipient)
        {
            return DbSet.Where(t => t.RecipientID == recipient.ID && t.IsDoner == false).ToList();
        }

        public IList<ContactPerson> GetAllForExport()
        {
            var returnVal = DbSet.Where(t => t.ID != null)
                .OrderBy(t => t.Code)
                .ThenBy(t => t.Name)
                .ToList();

            return returnVal;
        }
    }
}
