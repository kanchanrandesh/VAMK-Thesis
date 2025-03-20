namespace VAMK.FWMS.Models
{
    public class SystemConfiguration : EntityBase
    {
        private string _registrationName;
        public string RegistrationName
        {
            get { return _registrationName; }
            set
            {
                _registrationName = value;
                AuditReference = value;
            }
        }
        public TimeZone TimeZone { get; set; }
        public int? TimeZoneID { get; set; }      
        public string EmailServer { get; set; }
        public int? EmailServerPort { get; set; }
        public string EmailSenderGeneral { get; set; }
        public string EmailSenderGeneralPassword { get; set; }
        public string EmailSenderGeneralDisplayName { get; set; }     
        public int? MCFinanceApprovedByID { get; set; }   
    }
}
