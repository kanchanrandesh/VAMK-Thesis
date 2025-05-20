using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class GroupModel
    {
        public string id { get; set; }
        public string description { get; set; }
        public bool salesFullControl { get; set; }
        public bool isActive { get; set; }
        public string timeStamp { get; set; }
        public List<SelectObjectModel> assignedList { get; set; }
        public List<SelectObjectModel> notAssignedList { get; set; }

        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Group(GroupModel e)
        {
            return new VAMK.FWMS.Models.Group()
            {
                ID = Utility.ParseInt(e.id),
                Description = e.description,
                SalesFullControl = e.salesFullControl,
                IsActive = e.isActive,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator GroupModel(VAMK.FWMS.Models.Group e)
        {
            return new GroupModel()
            {
                id = e.ID.Value.ToString(),
                description = e.Description,
                isActive = e.IsActive,
                salesFullControl = e.SalesFullControl,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}