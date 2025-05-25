using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMK.FWMS.Models.Enums;

namespace VAMK.FWMS.WebSite.Models
{
    public class ContactPersonModel
    {
        public string @doner { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool isDoner { get; set; }
        public string donerId { get; set; }
        public string recipientId { get; set; }
        public string dateCreated { get; set; }
        public string timeStamp { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.ContactPerson(ContactPersonModel e)
        {
            return new VAMK.FWMS.Models.ContactPerson()
            {
                ID = Utility.ParseInt(e.id),
                Name = e.name,
                Code = e.code,
                PhoneNumber = e.phoneNumber,
                Mobile = e.mobile,
                Email = e.email,
                IsDoner = true,
                DonerID = Utility.ParseInt(e.donerId),
                RecipientID = Utility.ParseInt(e.recipientId),
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
            };
        }

        public static explicit operator ContactPersonModel(VAMK.FWMS.Models.ContactPerson e)
        {
            return new ContactPersonModel()
            {
                id = e.ID.Value.ToString(),
                name = e.Name,
                code = e.Code,
                phoneNumber = e.PhoneNumber,
                mobile = e.Mobile,
                email = e.Email,
                isDoner =true,
                donerId = e.DonerID.Value.ToString(),
                recipientId = e.RecipientID.Value.ToString(),                
                timeStamp = Utility.TimeStampToString(e.TimeStamp)        

            };
        }
    }
}