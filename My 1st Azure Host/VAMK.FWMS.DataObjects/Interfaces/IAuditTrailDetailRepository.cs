using VAMK.FWMS.Models;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.Interfaces
{
    public interface IAuditTrailDetailRepository : IRepository<AuditTrailDetail>
    {
        /// <summary>
        /// Get all Audit Trail Details for Audit Trail
        /// </summary>
        /// <param name="auditTrail"></param>
        /// <returns></returns>
        IList<AuditTrailDetail> GetAllFor(AuditTrail auditTrail);
    }
}
