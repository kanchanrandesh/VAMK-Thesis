namespace VAMK.FWMS.Models
{
    public class AuditTrailDetail : EntityBase
    {
        public AuditTrail AuditTrail { get; set; }
        public int? AuditTrailID { get; set; }
        public string EntityType { get; set; }
        public int? EntityID { get; set; }
        public string Property { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public Enums.AuditTrailAction Action { get; set; }
    }
}
