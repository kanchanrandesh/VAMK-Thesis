using System;
using System.Collections.Generic;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class DonerModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string dateCreated { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string locationCoordinates { get; set; }
        public List<ContactPersonModel> contactPersonList { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Doner(DonerModel e)
        {
            var contactPersonList = new List<VAMK.FWMS.Models.ContactPerson>();
            if (e.contactPersonList != null && e.contactPersonList.Count > 0)
            {
                foreach (var item in e.contactPersonList)
                {
                    contactPersonList.Add(new VAMK.FWMS.Models.ContactPerson()
                    {
                        Name = item.name,
                        Code = item.code,
                        PhoneNumber = item.phoneNumber,
                        Mobile = item.mobile,
                        Email = item.email,
                        IsDoner = true,
                        ID = !String.IsNullOrWhiteSpace(item.id) ? int.Parse(item.id) : 0
                    });
                }
            }

            return new VAMK.FWMS.Models.Doner()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,
                Address = e.address,
                Location = e.locationCoordinates,
                ContactPersonList = contactPersonList,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };


        }

        public static explicit operator DonerModel(VAMK.FWMS.Models.Doner e)
        {
            //Contact Person List
            var contactPersonList = new List<VAMK.FWMS.WebSite.Models.ContactPersonModel>();
            if (e.ContactPersonList != null && e.ContactPersonList.Count > 0)
            {
                foreach (var item in e.ContactPersonList)
                {
                    contactPersonList.Add(new ContactPersonModel()
                    {
                        id = item.ID.ToString(),
                        name = item.Name,
                        code = item.Code,
                        phoneNumber = item.PhoneNumber,
                        email = item.Email,
                        mobile = item.Mobile,
                    });
                }
            }

            return new DonerModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.Name,
                address = e.Address,
                locationCoordinates = e.Location,
                timeStamp = Utility.TimeStampToString(e.TimeStamp),
                contactPersonList = contactPersonList,
            };
        }
    }
}
