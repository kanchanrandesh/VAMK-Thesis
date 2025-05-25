namespace VAMK.FWMS.Models
{
    public class Department : EntityBase
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                AuditReference = value;
            }
        }
        public string Name { get; set; }
        public SystemUser AuthorizedOfficer { get; set; }
        public int? AuthorizedOfficerID { get; set; }
    }
}
