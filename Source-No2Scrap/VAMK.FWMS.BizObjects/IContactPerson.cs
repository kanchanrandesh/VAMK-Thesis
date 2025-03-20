using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects
{
    public interface IContactPerson : IBizObjectBase<ContactPerson>
    {
        
        /// <summary>
        /// Return list of Contacts for export
        /// </summary>
        /// <returns></returns>
        IList<ContactPerson> GetAllForExport();
    }
}
