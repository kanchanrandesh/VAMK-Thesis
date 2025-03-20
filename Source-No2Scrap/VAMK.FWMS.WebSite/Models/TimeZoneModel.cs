using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class TimeZoneModel
    {
        public string id { get; set; }
        public string key { get; set; }
        public string displayName { get; set; }
        public bool hasDayLightSavingTime { get; set; }
        public string timeStamp { get; set; }

        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.TimeZone(TimeZoneModel e)
        {
            return new VAMK.FWMS.Models.TimeZone()
            {
                ID = Utility.ParseInt(e.id),
                Key = e.key,
                DisplayName = e.displayName,
                HasDayLightSavingTime = e.hasDayLightSavingTime,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator TimeZoneModel(VAMK.FWMS.Models.TimeZone e)
        {
            return new TimeZoneModel()
            {
                id = e.ID.Value.ToString(),
                key = e.Key,
                displayName = e.DisplayName,
                hasDayLightSavingTime = e.HasDayLightSavingTime,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}