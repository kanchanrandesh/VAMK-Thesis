using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class CountryModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string timeStamp { get; set; }

        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Country(CountryModel e)
        {
            return new VAMK.FWMS.Models.Country()
            {
                ID = Utility.ParseInt(e.id),
                Name = e.name,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator CountryModel(VAMK.FWMS.Models.Country e)
        {
            return new CountryModel()
            {
                id = e.ID.Value.ToString(),
                name = e.Name,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}