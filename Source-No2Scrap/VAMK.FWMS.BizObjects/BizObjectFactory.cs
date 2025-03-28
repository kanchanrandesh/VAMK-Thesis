using VAMK.FWMS.BizObjects.Impl; 

namespace VAMK.FWMS.BizObjects
{
    public static class BizObjectFactory
    {
        public static ITimeZone GetTimeZoneBO()
        {
            return new TimeZoneBO();
        }
        public static ISystemConfiguration GetSystemConfigurationBO()
        {
            return new SystemConfigurationBO();
        }

        public static ICompany GetCompanyBO()
        {
            return new CompanyBO();
        }

        public static IEmployee GetEmployeeBO()
        {
            return new EmployeeBO();
        }
        public static IAuditTrail GetAuditTrailBO()
        {
            return new AuditTrailBO();
        }

        public static IAuditTrailDetail GetAuditTrailDetailBO()
        {
            return new AuditTrailDetailBO();
        }

        public static ICountry GetCountryBO()
        {
            return new CountryBO();
        }

        public static IContactPerson GetContactPersonBO()
        {
            return new ContactPersonBO();
        }

        public static IEmailOutBox GetEmailOutBoxBO()
        {
            return new EmailOutBoxBO();
        }

        public static ISentEmail GetSentEmailBO()
        {
            return new SentEmailBO();
        }

        public static IGroupEmployee GroupEmployeeBO()
        {
            return new GroupEmployeeBO();
        }

        public static IDepartment GetDepartmentBO()
        {
            return new DepartmentBO();
        }

        public static IGroup GetGroupBO()
        {
            return new GroupBO();
        }
        public static IGroupEmployee GetGroupEmployeeBO()
        {
            return new GroupEmployeeBO();
        }
        public static IGroupRule GetGroupRuleBO()
        {
            return new GroupRuleBO();
        }
        public static IRule GetRuleBO()
        {
            return new RuleBO();
        }
        public static IUnit GetUnitBO()
        {
            return new UnitBO();
        }

        //fwms
        public static IDoner GetDonerBO()
        {
            return new DonerBO();
        }
        public static IItem GetItemBO()
        {
            return new ItemBO();
        }
        public static IRecipient GetRecipientBO()
        {
            return new RecipientBO();
        }
    }
}
