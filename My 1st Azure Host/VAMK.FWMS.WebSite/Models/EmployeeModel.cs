using VAMK.FWMS.WebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAMK.FWMS.Models;

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

        public List<EmployeeRecipientModel> employeeRecipients { get; set; }
        public List<EmployeeDonerModel> employeeDoners { get; set; }

        public static implicit operator VAMK.FWMS.Models.Employee(EmployeeModel e)
        {
            var employeeRecipients = new List<RecipientUser>();
            foreach (var item in e.employeeRecipients)
            {
                employeeRecipients.Add(new RecipientUser
                {
                    ID = Utility.ParseInt(item.id),
                    TimeStamp = item.id != null ? Utility.StringToTimeStamp(item.timeStamp) : new byte[8],

                    EmployeeID = Utility.ParseInt(item.employeeId),
                    RecipientID = Utility.ParseInt(item.recipientId),
                });
            }
            var employeeDoners = new List<DonerUser>();
            foreach (var item in e.employeeDoners)
            {
                employeeDoners.Add(new DonerUser
                {
                    ID = Utility.ParseInt(item.id),
                    TimeStamp = item.id != null ? Utility.StringToTimeStamp(item.timeStamp) : new byte[8],

                    EmployeeID = Utility.ParseInt(item.employeeId),
                    DonerID = Utility.ParseInt(item.donerId),
                });
            }
            return new VAMK.FWMS.Models.Employee()
            {
                ID = Utility.ParseInt(e.id),
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
                IsLeagalOfficer = e.isLeagalOfficer,
                EmployeeRecipients = employeeRecipients,
                EmployeeDoners = employeeDoners,
            };
        }

        public static explicit operator EmployeeModel(VAMK.FWMS.Models.Employee e)
        {
            var employeeRecipients = new List<EmployeeRecipientModel>();
            foreach (var item in e.EmployeeRecipients)
            {
                employeeRecipients.Add(new EmployeeRecipientModel
                {
                    id = item.ID.Value.ToString(),
                    timeStamp = Utility.TimeStampToString(item.TimeStamp),

                    employeeId = item.EmployeeID.ToString(),
                    recipientId = item.RecipientID.ToString(),
                });
            }
            var employeeDoners = new List<EmployeeDonerModel>();
            foreach (var item in e.EmployeeDoners)
            {
                employeeDoners.Add(new EmployeeDonerModel
                {
                    id = item.ID.Value.ToString(),
                    timeStamp = Utility.TimeStampToString(item.TimeStamp),

                    employeeId = item.EmployeeID.ToString(),
                    donerId = item.DonerID.ToString(),
                });
            }

            return new EmployeeModel()
            {
                id = e.ID.Value.ToString(),
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
                isLeagalOfficer = e.IsLeagalOfficer,
                employeeDoners = employeeDoners,
                employeeRecipients = employeeRecipients
            };
        }
    }
}