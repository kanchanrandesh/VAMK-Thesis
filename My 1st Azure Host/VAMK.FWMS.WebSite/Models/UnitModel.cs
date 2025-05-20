using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class UnitModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Unit(UnitModel e)
        {
            return new VAMK.FWMS.Models.Unit()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                UnitName = e.name,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator UnitModel(VAMK.FWMS.Models.Unit e)
        {
            return new UnitModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.UnitName,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}