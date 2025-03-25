using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class ItemModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string code { get; set; }
        public string name { get; set; }      
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Item(ItemModel e)
        {
            return new VAMK.FWMS.Models.Item()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,                
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator ItemModel(VAMK.FWMS.Models.Item e)
        {
            return new ItemModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.Name,               
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}
