namespace VAMK.FWMS.Models.SearchQueries
{
    public class UnitSearchQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DepartmentID { get; set; }
        public string SearchText { get; set; }
       
    }
}
