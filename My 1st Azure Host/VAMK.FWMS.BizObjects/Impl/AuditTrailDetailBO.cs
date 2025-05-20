using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class AuditTrailDetailBO : BizObjectBase<AuditTrailDetail>, IAuditTrailDetail
    {
        #region Vars

        IAuditTrailDetailRepository auditTrailDetailRepository;

        #endregion

        public override AuditTrailDetail Create()
        {
            return new AuditTrailDetail();
        }

        #region Property Fileld

        public IAuditTrailDetailRepository Repository { get { return auditTrailDetailRepository; } }

        #endregion

        #region Costructor

        public AuditTrailDetailBO() : this(new AuditTrailDetailRepository()) { }

        public AuditTrailDetailBO(IAuditTrailDetailRepository repository)
            : base(repository)
        {
            auditTrailDetailRepository = repository;
        }

        #endregion

        #region Get All For

        /// <summary>
        /// Return list of Audit Trail Details for an Audit Trail
        /// </summary>
        /// <param name="auditTrail"></param>
        /// <returns></returns>
        public IList<AuditTrailDetail> GetAllFor(AuditTrail auditTrail)
        {
            return Repository.GetAllFor(auditTrail);
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
