using System;
using System.Collections.Generic;

namespace VAMK.FWMS.Models
{
    public class Employee : EntityBase
    {

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                AuditReference = value;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Company Company { get; set; }
        public int? CompanyID { get; set; }
        public string Designation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int? UnSuccessfulLoginAttempts { get; set; }
        public DateTime? PasswordResetDate { get; set; }
        public bool IsDoner { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsSalesEngineer { get; set; }
        public bool IsPreSaleEngineer { get; set; }
        public bool IsProjectManager { get; set; }
        public bool IsBizDeveloper { get; set; }
        public bool IsTechnicalPerson { get; set; }
        public bool IsLeagalOfficer { get; set; }
        public IList<EmployeeDoner> EmployeeDoners { get; set; } = new List<EmployeeDoner>();
        public IList<EmployeeRecipient> EmployeeRecipients { get; set; } = new List<EmployeeRecipient>();

    }
}
