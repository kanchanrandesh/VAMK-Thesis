namespace VAMK.FWMS.Models
{
    public class GroupEmployee : EntityBase
    {
        public Group Group { get; set; }
        public int? GroupID { get; set; }
        public Employee Employee { get; set; }       
        public int? EmployeeID { get; set; }
    }
}
