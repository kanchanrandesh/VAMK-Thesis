using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class DonerModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string locationCoordinates { get; set; }       
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Doner(DonerModel e)
        {
            return new VAMK.FWMS.Models.Doner()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,
                Address = e.address,
                Location= e.locationCoordinates,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator DonerModel(VAMK.FWMS.Models.Doner e)
        {
            return new DonerModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.Name,
                address = e.Address,
                locationCoordinates = e.Location,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}
