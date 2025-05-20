namespace VAMK.FWMS.Models
{
    public class Country : EntityBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                AuditReference = value;
            }
        }
    }
}
