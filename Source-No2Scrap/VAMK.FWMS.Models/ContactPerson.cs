using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAMK.FWMS.Models.Enums;

namespace VAMK.FWMS.Models
{
    public class ContactPerson : EntityBase
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
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public ContactPersonType ContactPersonType { get; set; }
        public int? ContactableSourceID { get; set; }

    }
}