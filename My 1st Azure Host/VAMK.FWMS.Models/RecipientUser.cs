using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class RecipientUser : EntityBase
    {
        public Employee Employee { get; set; }
        public int? EmployeeID { get; set; }
        public Recipient Recipient  { get; set; }
        public int? RecipientID { get; set; }
    }
}
