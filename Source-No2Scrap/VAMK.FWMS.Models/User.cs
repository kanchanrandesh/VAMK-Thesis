using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class SystemUser : EntityBase
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
        public string Designation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public int? UnSuccessfulLoginAttempts { get; set; }
        public DateTime? PasswordResetDate { get; set; }
        public bool IsDoner { get; set; }
        public bool IsRecipient { get; set; }
        public IList<UserDoner> UserDoners { get; set; } = new List<UserDoner>();
        public IList<UserRecipient> UserRecipients { get; set; } = new List<UserRecipient>();
    }
}
