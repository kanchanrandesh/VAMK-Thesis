using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class FormRule : EntityBase
    {
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                AuditReference = value;
            }
        }
        public string Description { get; set; }
    }
}
