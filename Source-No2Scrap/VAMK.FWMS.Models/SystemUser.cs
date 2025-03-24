using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class SystemUser : EntityBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                AuditReference = value;
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }      
        public string Email { get; set; }       
        public string Password { get; set; }
        public int? DonerID { get; set; }
        public Doner Doner { get; set; }
        public int? RecipientID { get; set; }
        public Recipient Recipient { get; set; }
        public string Designation { get; set; }
        public Models.Enums.SystemUserType SystemUserType { get; set; }        
    }
}
