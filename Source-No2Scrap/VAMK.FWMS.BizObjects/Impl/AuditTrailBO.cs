using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class AuditTrailBO : BizObjectBase<AuditTrail>, IAuditTrail
    {
        #region Vars

        IAuditTrailRepository auditTrailRepository;

        #endregion

        public override AuditTrail Create()
        {
            return new AuditTrail();
        }

        #region Property Fileld

        public IAuditTrailRepository Repository { get { return auditTrailRepository; } }

        #endregion

        #region Costructor

        public AuditTrailBO() : this(new AuditTrailRepository()) { }

        public AuditTrailBO(IAuditTrailRepository repository)
            : base(repository)
        {
            auditTrailRepository = repository;
        }

        #endregion

        #region Object Type

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }

        #endregion
    }
}
