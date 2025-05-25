using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class DepartmentModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string authorizedOfficerId { get; set; }

        public string authorizedOfficerName { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Department(DepartmentModel e)
        {
            return new VAMK.FWMS.Models.Department()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,
                AuthorizedOfficerID = Utility.ParseInt(e.authorizedOfficerId),
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator DepartmentModel(VAMK.FWMS.Models.Department e)
        {
            return new DepartmentModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.Name,
                authorizedOfficerId = e.AuthorizedOfficerID != null ? e.AuthorizedOfficerID.ToString() : string.Empty,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}