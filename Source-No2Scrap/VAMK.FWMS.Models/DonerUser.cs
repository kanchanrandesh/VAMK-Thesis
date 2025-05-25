using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class UserDoner : EntityBase
    {
        public SystemUser SystemUser { get; set; }
        public int? SystemUserID { get; set; }
        public Doner Doner { get; set; }
        public int? DonerID { get; set; }
    }
}
