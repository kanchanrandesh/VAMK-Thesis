using VAMK.FWMS.DataObjects.Insfratructures;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.DataObjects
{
    public class AuditTrailRepository : RepositoryBase<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository() : this(new RepositoryContext()) { }

        public AuditTrailRepository(IRepositoryContext context) : base(context) { }

        public override AuditTrail GetSingle(System.Linq.Expressions.Expression<System.Func<AuditTrail, bool>> whereCondition)
        {
            var returnVal = this.DbSet.Where(whereCondition).FirstOrDefault();
            returnVal.DetailList = new AuditTrailDetailRepository().GetAllFor(returnVal);

            return returnVal;
        }
    }
}
