namespace VAMK.FWMS.Models
{
    public class AssociatedCompany : EntityBase
    {
       
        public int? OrganizationID { get; set; }
        public Company Company { get; set; }
        private int? _companyID;
        public int? CompanyID
        {
            get { return _companyID; }
            set
            {
                _companyID = value;
                AuditReference = value != null ? value.Value.ToString() : string.Empty;
            }
        }
        public Employee PrimaryRepresentative { get; set; }
        public int? PrimaryRepresentativeID { get; set; }
        public Employee SecondaryRepresentative { get; set; }
        public int? SecondaryRepresentativeID { get; set; }
    }
}
