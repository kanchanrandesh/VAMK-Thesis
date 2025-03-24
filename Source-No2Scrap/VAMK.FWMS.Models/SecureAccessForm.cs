using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class SecureAccessForm : EntityBase
    {
        private string _formCode;
        public string FormCode
        {
            get { return _formCode; }
            set
            {
                _formCode = value;
                AuditReference = value;
            }
        }
        public string FormName { get; set; }
    }
}
