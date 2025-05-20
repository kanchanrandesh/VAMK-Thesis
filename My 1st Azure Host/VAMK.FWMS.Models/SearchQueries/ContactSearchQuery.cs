namespace VAMK.FWMS.Models.SearchQueries
{
    public class ContactSearchQuery
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string JobRole { get; set; }
        public int? OrganizationID { get; set; }
        public int? IndustryID { get; set; }
        public int? EventID { get; set; }
        public int? AccountManagerID { get; set; }
        public string SearchText { get; set; }
        public string DesignationCategoryIDList { get; set; }
        public string DesignationCategoryID { get; set; }

    }
}
