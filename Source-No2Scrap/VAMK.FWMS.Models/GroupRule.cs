namespace VAMK.FWMS.Models
{
    public class GroupRule : EntityBase
    {
        public Group Group { get; set; }
        public int? GroupID { get; set; }
        public Rule Rule { get; set; }
        private int? _ruleID;
        public int? RuleID
        {
            get { return _ruleID; }
            set
            {
                _ruleID = value;
                AuditReference = value != null ? value.Value.ToString() : string.Empty;
            }
        }
    }
}
