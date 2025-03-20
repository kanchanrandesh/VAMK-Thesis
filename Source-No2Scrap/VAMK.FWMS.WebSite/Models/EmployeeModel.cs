using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class EmployeeModel
    {
        public string id { get; set; }
        public string timeStamp { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string extension { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool isActive { get; set; }
        public bool eligibleForMedicalClaims { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string temporaryPassword { get; set; }
        public bool temporaryPasswordEnabled { get; set; }
        public string companyId { get; set; }
        public string jobCategoryId { get; set; }
        public bool isLocked { get; set; }
        public string unSuccessfulLoginAttempts { get; set; }
        public bool isRelationshipManager { get; set; }
        public bool isSalesManager { get; set; }
        public bool isSalesEngineer { get; set; }
        public bool isPreSaleEngineer { get; set; }
        public bool isProjectManager { get; set; }
        public bool isBizDeveloper { get; set; }
        public bool isTechnicalPerson { get; set; }
        public bool isLeagalOfficer { get; set; }
        public string displayName { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public static implicit operator VAMK.FWMS.Models.Employee(EmployeeModel e)
        {
            return new VAMK.FWMS.Models.Employee()
            {
                ID = Utility.ParseInt(e.id),
                Title = (VAMK.FWMS.Models.Enums.IndividualTitle)Enum.Parse(typeof(VAMK.FWMS.Models.Enums.IndividualTitle), e.title),
                Code = e.code,
                FirstName = e.firstName,
                LastName = e.lastName,
                Phone = e.phone,
                
                Mobile = e.mobile,
                Email = e.email,
                IsActive = e.isActive,              
                UserName = e.userName,
                Password = e.password,
                CompanyID = Utility.ParseInt(e.companyId),               
                TimeStamp = e.id != null ? Utility.StringToTimeStamp(e.timeStamp) : new byte[8],
                IsLocked = e.isLocked,
                UnSuccessfulLoginAttempts = Utility.ParseInt(e.unSuccessfulLoginAttempts),
                IsDoner = e.isRelationshipManager,
                IsRecipient = e.isSalesManager,
                IsSalesEngineer = e.isSalesEngineer,
                IsPreSaleEngineer = e.isPreSaleEngineer,
                IsProjectManager = e.isProjectManager,
                IsBizDeveloper = e.isBizDeveloper,
                IsTechnicalPerson = e.isTechnicalPerson,
                IsLeagalOfficer = e.isLeagalOfficer
            };
        }

        public static explicit operator EmployeeModel(VAMK.FWMS.Models.Employee e)
        {
            return new EmployeeModel()
            {
                id = e.ID.Value.ToString(),
                title = e.Title.ToString(),
                code = e.Code,
                firstName = e.FirstName,
                lastName = e.LastName,
                phone = e.Phone,                
                mobile = e.Mobile,
                email = e.Email,
                isActive = e.IsActive,                
                userName = e.UserName,
                password = e.Password,
                companyId = e.CompanyID != null ? e.CompanyID.Value.ToString() : string.Empty,               
                timeStamp = Utility.TimeStampToString(e.TimeStamp),
                displayName = e.FirstName + " " + e.LastName,
                isLocked = e.IsLocked,
                unSuccessfulLoginAttempts = e.UnSuccessfulLoginAttempts != null ? e.UnSuccessfulLoginAttempts.Value.ToString() : string.Empty,
                isRelationshipManager = e.IsDoner,
                isSalesManager = e.IsRecipient,
                isSalesEngineer = e.IsSalesEngineer,
                isPreSaleEngineer = e.IsPreSaleEngineer,
                isProjectManager = e.IsProjectManager,
                isBizDeveloper = e.IsBizDeveloper,
                isTechnicalPerson = e.IsTechnicalPerson,
                isLeagalOfficer = e.IsLeagalOfficer
            };
        }
    }
}