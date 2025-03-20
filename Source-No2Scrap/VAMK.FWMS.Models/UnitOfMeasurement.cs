namespace VAMK.FWMS.Models
{
    public class UnitOfMeasurement : EntityBase
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
        public string UnitName { get; set; }
       
    }
}
