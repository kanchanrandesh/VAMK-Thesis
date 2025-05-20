using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class DonerUser : EntityBase
    {
        public Employee Employee  { get; set; }
        public int? EmployeeID { get; set; }
        public Doner Doner { get; set; }
        public int? DonerID { get; set; }
    }
}
