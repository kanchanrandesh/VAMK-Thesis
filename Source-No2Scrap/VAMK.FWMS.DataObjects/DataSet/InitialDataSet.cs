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
                    new Rule { Code = "CONTCTADED", Description = "Contact Add / Edit",User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "CONTCTVIEW", Description = "Contact View",User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DASHBDVIEW", Description = "Dashboard View",User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DONATVIEW", Description = "Donation View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DONATADDED", Description = "Donation Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DONATRECE", Description = "Donation Collect", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DONERVIEW", Description = "Donor Add-Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "DONERADED", Description = "Donor View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USERVIEW", Description = "User View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USERADED", Description = "User Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USGROPVIEW", Description = "User View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "USGROPADED", Description = "USGROPADED", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ITEMVIEW", Description = "Item View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "ITEMADDED", Description = "Item Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "RECPTVIEW", Description = "Recipient View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "RECPTADED", Description = "Recipient Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "REQUESVIEW", Description = "Request View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "REQUESADED", Description = "Request Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "UNITVIEW", Description = "Unit View", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Rule { Code = "UNITADED", Description = "Unit Add / Edit", User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
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
                    new VAMK.FWMS.Models.Group { Description = "Coordinators",       IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new VAMK.FWMS.Models.Group { Description = "Donor Users",        IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Head of Department",   IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Finance Managers",     IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    //new VAMK.FWMS.Models.Group { Description = "Finance Officers",     IsActive = true, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                 };
            groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();

            #endregion

            #region Group Users

            var groupEmployees = new List<VAMK.FWMS.Models.GroupUser>
                 {
                    new VAMK.FWMS.Models.GroupUser { GroupID = 1, EmployeeID = 1, User = "Seed", DateCreated = DateTime.Now, DateModified = DateTime.Now }
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
