using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class Recipient : EntityBase
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
        public string Address { get; set; }
        public IList<ContactPerson> ContactPersonList { get; set; }
    }
}