using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class ItemModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string itemCategory { get; set; }
        public string unitID { get; set; }
        public string unitName { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Item(ItemModel e)
        {
            return new VAMK.FWMS.Models.Item()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,
                ItemCategory = (VAMK.FWMS.Models.Enums.ItemCategory)Enum.Parse(typeof(VAMK.FWMS.Models.Enums.ItemCategory), e.itemCategory),
                UnitID = Utility.ParseInt(e.unitID),
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
                itemCategory = e.ItemCategory.ToString(),
                unitID = e.UnitID != null ? e.UnitID.Value.ToString() : string.Empty,
                unitName = e.Unit != null ? e.Unit.UnitName : string.Empty,               
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}
