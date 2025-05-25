namespace VAMK.FWMS.Models
{
    public class GroupUser : EntityBase
    {
        public Group Group { get; set; }
        public int? GroupID { get; set; }
        public SystemUser Employee { get; set; }       
        public int? EmployeeID { get; set; }
    }
}
