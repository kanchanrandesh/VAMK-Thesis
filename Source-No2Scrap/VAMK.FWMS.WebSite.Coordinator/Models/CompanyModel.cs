using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class CompanyModel
    {
        public string id { get; set; }
        public string code { get; set; }
        public string prefixCode { get; set; }
        public string name { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string addressLine3 { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string email { get; set; }
        public string timeStamp { get; set; }

        public bool hasSelected { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Company(CompanyModel e)
        {
            return new VAMK.FWMS.Models.Company()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                PrefixCode = e.prefixCode,
                Name = e.name,
                AddressLine1 = e.addressLine1,
                AddressLine2 = e.addressLine2,
                AddressLine3 = e.addressLine3,
                Phone1 = e.phone1,
                Phone2 = e.phone2,
                Email = e.email,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };
        }

        public static explicit operator CompanyModel(VAMK.FWMS.Models.Company e)
        {
            return new CompanyModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                prefixCode = e.PrefixCode,
                name = e.Name,
                addressLine1 = e.AddressLine1,
                addressLine2 = e.AddressLine2,
                addressLine3 = e.AddressLine3,
                phone1 = e.Phone1,
                phone2 = e.Phone2,
                email = e.Email,
                timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}