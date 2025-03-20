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
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }       
        public string phoneNumber { get; set; }
      
        public string mobile { get; set; }
       
        public string email { get; set; }
        public ContactPersonType contactPersonType { get; set; }
        public int? contactableSourceID { get; set; }
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
                ContactPersonType = e.contactPersonType,
                ContactableSourceID = e.contactableSourceID,
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
            };
        }

        public static explicit operator ContactPersonModel(VAMK.FWMS.Models.ContactPerson e)
        {
            return new ContactPersonModel()
            {
                id = e.ID.Value.ToString(),
                //    contactPersonType = e.ContactPersonType,
                //    firstName = e.FirstName,
                //    lastName = e.LastName,
                //    phone = e.PhoneNumber,
                //    extension = e.Extension,
                //    mobile = e.Mobile,
                //    mobile2 = e.Mobile2,
                //    email = e.Email,
                //    email2 = e.Email2,
                //    secretary = e.Secretary,
                //    secretaryEmail = e.SecretaryEmail,
                //    secretaryMobile = e.SecretaryMobile,
                //    addressLine1 = e.AddressLine1,
                //    addressLine2 = e.AddressLine2,
                //    addressLine3 = e.AddressLine3,
                //    countryId = e.CountryID != null ? e.CountryID.Value.ToString() : string.Empty,
                //    organizationId = e.OrganizationID != null ? e.OrganizationID.Value.ToString() : string.Empty,
                //    industryList = e.IndustryList,
                //    jobRole = e.JobRole,
                //    department = e.Department,
                //    designationCategoryId = e.DesignationCategoryID != null ? e.DesignationCategoryID.Value.ToString() : string.Empty,
                //    accountManagerId = e.AccountManagerID != null ? e.AccountManagerID.Value.ToString() : string.Empty,
                //    accountManager = e.AccountManager != null ? e.AccountManager.FirstName + " " + e.AccountManager.LastName : string.Empty,
                //    accountManagerCode = e.AccountManager != null ? e.AccountManager.Code : string.Empty,
                //    birthYear = e.BirthYear != null ? e.BirthYear.Value.ToString() : string.Empty,
                //    birthMonth = e.BirthMonth != null ? e.BirthMonth.Value.ToString() : string.Empty,
                //    birthDate = e.BirthDate != null ? e.BirthDate.Value.ToString() : string.Empty,
                //    notes = e.Notes,
                //    photoUrl = e.PhotoURL,
                //    timeStamp = Utility.TimeStampToString(e.TimeStamp)
            };
        }
    }
}