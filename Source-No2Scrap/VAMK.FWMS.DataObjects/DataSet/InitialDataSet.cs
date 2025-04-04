using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;

namespace VAMK.FWMS.DataObjects.DataSet
{
    internal class InitialDataSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void InsertDataSet(Context.FWMSDbContext context)
        {
            #region Rules

            var rules = new List<Rule>
                {
                    new Rule { Code = "IOUREQUEST", Description = "IOU Request",                           User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "IOUAPPROVL", Description = "IOU Approval",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "IOUPAYMENT", Description = "IOU Payment",                           User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PETREQUEST", Description = "Petty Cash Request",                    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PETAPPROVL", Description = "Petty Cash Approval",                   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PETDISBRST", Description = "Petty Cash Disbursment",                User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PETREIMBRT", Description = "Petty Cash Reimbursment",               User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PETTYREPOT", Description = "Petty Cash Report",                     User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EVENTSVIEW", Description = "Event View",                            User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EVENTADEDT", Description = "Event Add / Edit",                      User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EVENTDELET", Description = "Event Delete",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EVENTHOMET", Description = "Event Home",                            User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTCTVIEW", Description = "Contact View",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTCTADED", Description = "Contact Add / Edit",                    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTCTDELE", Description = "Contact Delete",                        User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ORGANZVIEW", Description = "Organization View",                     User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ORGANZADED", Description = "Organization Add / Edit",               User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ORGANZDELE", Description = "Organization Delete",                   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "COMPNYVIEW", Description = "Company View",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "COMPNYADED", Description = "Company Add / Edit",                    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DEPRTMVIEW", Description = "Department View",                       User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DEPRTMADED", Description = "Department Add / Edit",                 User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PROJCTVIEW", Description = "Project View",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "PROJCTADED", Description = "Project Add / Edit",                    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTRYVIEW", Description = "Country View",                          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTRYADED", Description = "Country Add / Edit",                    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DSGCATVIEW", Description = "Designation Category View",             User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DSGCATADED", Description = "Designation Category Add / Edit",       User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "INDSTRVIEW", Description = "Industry Category View",                User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "INDSTRADED", Description = "Industry Category Add / Edit",          User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EXPNCTVIEW", Description = "Expense Category View",                 User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EXPNCTADED", Description = "Expense Category Add / Edit",           User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "SYSTMCONFG", Description = "System Configuration",                  User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "TIMZNEVIEW", Description = "Time Zone View",                        User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "TIMZNEADED", Description = "Time Zone Add / Edit",                  User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EMPLYEVIEW", Description = "Employee View",                         User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "EMPLYEADED", Description = "Employee Add / Edit",                   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USGROPVIEW", Description = "User Group View",                       User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USGROPADED", Description = "User Group Add / Edit",                 User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "UNITVIEWFN", Description = "Unit View",                             User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "UNITADDEDT", Description = "Unit Add / Edit",                       User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "GENERALACC", Description = "General Access",                        User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "MEDREQUEST", Description = "Medical Claim Request",                 User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ORGCATVIEW", Description = "Organization Category View",            User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ORGCATADED", Description = "Organization Category Add / Edit",      User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "FINPERVIEW", Description = "Financial Period View",                 User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "FINPERADED", Description = "Financial Period Add / Edit",           User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "FINYERVIEW", Description = "Financial Year View",                   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "FINYERADED", Description = "Financial Year Add / Edit",             User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONLVLVIEW", Description = "Confidence Level Criteria View",        User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONLVLADED", Description = "Confidence Level Criteria Add / Edit",  User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "OPPOTUVIEW", Description = "Opportunity View",                      User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "OPPOTUHOME", Description = "Opportunity Home",                      User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "SBUNITVIEW", Description = "SBU View",                              User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "SBUNITADED", Description = "SBU Add / Edit",                        User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "TIMSHTVIEW", Description = "Time Sheet View",                       User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },

                 };
            rules.ForEach(s => context.Rules.Add(s));
            context.SaveChanges();

            #endregion

            #region Companies

            var companies = new List<VAMK.FWMS.Models.Company>
                 {
                    new VAMK.FWMS.Models.Company { Code = "FWMSK", PrefixCode = "KR", Name = "Vaasa Universiy of Applied sciences",     AddressLine1 = "Wolffintie 30", AddressLine2 = "65200 ", AddressLine3 = "Vaasa", Phone1 = "+358 207 663 300", Phone2 = "+358 207 663 300", Email = "info@fwms.com",   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            companies.ForEach(s => context.Companies.Add(s));
            context.SaveChanges();

            #endregion

            #region Users

            var users = new List<VAMK.FWMS.Models.Employee>
                 {
                    /* 01 */ new VAMK.FWMS.Models.Employee { Code = "KR", FirstName = "KRWA",         LastName = "Wickrama Arachchi",   UserName = "KR",       Password = "12193117185192241182168491951532261051193897", Email = "kanchan.arachchi@gmail.com",  Phone ="+94 77 32 33417", Mobile = "+94 77 32 33 417", CompanyID = 1, IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                   };
            users.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            #endregion

            #region Groups

            var groups = new List<VAMK.FWMS.Models.Group>
                 {
                    new VAMK.FWMS.Models.Group { Description = "Administrators",       IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                    //new VAMK.FWMS.Models.Group { Description = "General Users",        IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Head of Department",   IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Finance Managers",     IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Finance Officers",     IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();

            #endregion

            #region Group Users

            var groupEmployees = new List<VAMK.FWMS.Models.GroupEmployee>
                 {
                    new VAMK.FWMS.Models.GroupEmployee { GroupID = 1, EmployeeID = 1, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            groupEmployees.ForEach(s => context.GroupEmployees.Add(s));
            context.SaveChanges();

            #endregion

            #region Group Rules
            var groupRules = new List<VAMK.FWMS.Models.GroupRule>();
            for (int i = 1; i <= rules.Count; i++)
            {
                groupRules.Add(
                    new VAMK.FWMS.Models.GroupRule { GroupID = 1, RuleID = i, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 );
            }
            groupRules.ForEach(s => context.GroupRules.Add(s));
            context.SaveChanges();

            #endregion

            #region Countries

            var countries = new List<Country>
                {
                    //new Country { Name = "Australia", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "Canada",    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "China",     User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "France",    User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "Germany",   User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "India",     User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "Japan",     User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "Malaysia",  User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new Country { Name = "Singapore", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Country { Name = "Sri Lanka", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            #endregion

            #region Time Zones

            var timeZones = new List<VAMK.FWMS.Models.TimeZone>
                 {
                    new VAMK.FWMS.Models.TimeZone { Key ="Sri Lanka Standard Time", DisplayName = "(UTC+05:30) Sri Jayawardenepura", HasDayLightSavingTime = false, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            timeZones.ForEach(s => context.TimeZones.Add(s));
            context.SaveChanges();

            #endregion

            #region System Configurations

            var systemConfigurations = new List<SystemConfiguration>
            {
                new SystemConfiguration { RegistrationName = "Kanchan Randesh", TimeZoneID = 1, EmailSenderGeneral="kanchan.arachchi@gmail.com", EmailSenderGeneralDisplayName = "FWMS", EmailSenderGeneralPassword = "OvnK3496*", EmailServer = "mail.asas.com", EmailServerPort = 25, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now}
            };
            systemConfigurations.ForEach(s => context.SystemConfiguration.Add(s));
            context.SaveChanges();


            #endregion            

            #region Departments

            var departments = new List<VAMK.FWMS.Models.Department>
                 {
                    new VAMK.FWMS.Models.Department { Code = "ADMN", Name = "Administration",          AuthorizedOfficerID = 1,  User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }

                 };
            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();

            #endregion

            #region Units

            //var units = new List<VAMK.FWMS.Models.Unit>
            //     {
            //        new VAMK.FWMS.Models.Unit { Code = "ADMINS", Name = "Administration",      AuthorizedOfficerID = 14, DepartmentID = 1, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
            //        new VAMK.FWMS.Models.Unit { Code = "CHRMOF", Name = "Chairman's Office",   AuthorizedOfficerID = 16, DepartmentID = 2, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
            //        new VAMK.FWMS.Models.Unit { Code = "FINANC", Name = "Finance & Accounts",  AuthorizedOfficerID = 5,  DepartmentID = 3, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
            //        new VAMK.FWMS.Models.Unit { Code = "HRESOR", Name = "Human Resources",     AuthorizedOfficerID = 13, DepartmentID = 4, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
            //        new VAMK.FWMS.Models.Unit { Code = "LEGALD", Name = "Legal",               AuthorizedOfficerID = 3,  DepartmentID = 5, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
            //        new VAMK.FWMS.Models.Unit { Code = "SALEMK", Name = "Sales & Marketing",   AuthorizedOfficerID = 4,  DepartmentID = 7, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
            //     };
            //units.ForEach(s => context.Units.Add(s));
            //context.SaveChanges();

            #endregion

            #region Sequence Number

            var sequenceNumber = new List<VAMK.FWMS.Models.SequenceNumber>
                 {
                    new VAMK.FWMS.Models.SequenceNumber { Type = Models.Enums.InventoryEffectedby.Donation.ToString(), Prefix = "DON" , LastNumber = 1 },
                    new VAMK.FWMS.Models.SequenceNumber { Type = Models.Enums.InventoryEffectedby.Request.ToString(), Prefix = "REQ" , LastNumber = 1 }

                 };
            sequenceNumber.ForEach(s => context.SequenceNumber.Add(s));
            context.SaveChanges();

            #endregion
        }
    }
}
