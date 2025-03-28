using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IContactPersonRepository : IRepository<ContactPerson>
    {       

        /// <summary>
        /// Get all contacts for export
        /// </summary>
        /// <returns></returns>
        IList<ContactPerson> GetAllForExport();

        /// <summary>
        /// Get all contact persons for Doner
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        IList<ContactPerson> GetAllFor(Doner doner);


    }
}
