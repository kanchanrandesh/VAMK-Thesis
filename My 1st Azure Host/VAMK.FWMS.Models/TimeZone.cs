namespace VAMK.FWMS.Models
{
    public class TimeZone : EntityBase
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                AuditReference = value;
            }
        }
        public string DisplayName { get; set; }
        public bool HasDayLightSavingTime { get; set; }
    }
}
