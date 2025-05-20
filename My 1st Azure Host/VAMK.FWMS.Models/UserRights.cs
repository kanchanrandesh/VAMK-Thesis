using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class UserRights : EntityBase
    {
        public SystemUser SystemUser { get; set; }
        public int? SystemUserID { get; set; }
        public FormRule SecureAccessForm { get; set; }
        public int? SecureAccessFormID { get; set; }
    }
}
