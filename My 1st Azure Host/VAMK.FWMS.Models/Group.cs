namespace VAMK.FWMS.Models
{
    public class Group : EntityBase
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                AuditReference = value;
            }
        }
        public bool SalesFullControl { get; set; }
        public bool IsActive { get; set; }
    }
}
