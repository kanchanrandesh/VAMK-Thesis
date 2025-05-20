using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class AuditTrailDetailRepository : RepositoryBase<AuditTrailDetail>, IAuditTrailDetailRepository
    {
        public AuditTrailDetailRepository() : this(new RepositoryContext()) { }

        public AuditTrailDetailRepository(IRepositoryContext context) : base(context) { }

        public System.Collections.Generic.IList<AuditTrailDetail> GetAllFor(AuditTrail auditTrail)
        {
            return DbSet.Where(t => t.AuditTrailID == auditTrail.ID).ToList();
        }
    }
}
