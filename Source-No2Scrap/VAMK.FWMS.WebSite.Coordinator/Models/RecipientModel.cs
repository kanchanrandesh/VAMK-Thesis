using System;
using System.Collections.Generic;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Models
{
    public class RecipientModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string dateCreated { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string location { get; set; }
        public List<ContactPersonModel> contactPersonList { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Recipient(RecipientModel e)
        {
            var contactPersonList = new List<VAMK.FWMS.Models.ContactPerson>();
            if (e.contactPersonList != null && e.contactPersonList.Count > 0)
            {
                foreach (var item in e.contactPersonList)
                {
                    contactPersonList.Add(new VAMK.FWMS.Models.ContactPerson()
                    {
                        Code = item.code,
                        Name = item.name,
                        PhoneNumber = item.phoneNumber,
                        Mobile = item.mobile,
                        Email = item.email,
                        IsDoner= false,
                        ID = !String.IsNullOrWhiteSpace(item.id) ? int.Parse(item.id) : 0
                    });
                }
            }

            return new VAMK.FWMS.Models.Recipient()
            {
                ID = Utility.ParseInt(e.id),
                Code = e.code,
                Name = e.name,
                Address = e.address,                
                ContactPersonList = contactPersonList,
                Location = e.location,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8]
            };


        }

        public static explicit operator RecipientModel(VAMK.FWMS.Models.Recipient e)
        {
            //Contact Person List
            var contactPersonList = new List<VAMK.FWMS.WebSite.Models.ContactPersonModel>();
            if (e.ContactPersonList != null && e.ContactPersonList.Count > 0)
            {
                foreach (var item in e.ContactPersonList)
                {
                    contactPersonList.Add(new ContactPersonModel()
                    {
                        code = item.Code,
                        id = item.ID.ToString(),
                        name = item.Name,
                        phoneNumber = item.PhoneNumber,
                        email = item.Email,
                        mobile = item.Mobile,
                    });
                }
            }

            return new RecipientModel()
            {
                id = e.ID.Value.ToString(),
                code = e.Code,
                name = e.Name,
                address = e.Address,                
                timeStamp = Utility.TimeStampToString(e.TimeStamp),
                contactPersonList = contactPersonList,
            };
        }
    }
}
