namespace VAMK.FWMS.Models.SearchQueries
{
    public class EmployeeSearchQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool? IsActive { get; set; }
        public Company Company { get; set; }
        public int? CompanyID { get; set; }
        public string SearchText { get; set; }
    }
}
