using System;
using System.Collections.Generic;

namespace VAMK.FWMS.Models
{
    public class AuditTrail : EntityBase
    {
        public string EntityType { get; set; }
        public int? EntityID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? Date { get; set; }
        public Enums.AuditTrailAction Action { get; set; }
        public IList<AuditTrailDetail> DetailList { get; set; }
    }
}
