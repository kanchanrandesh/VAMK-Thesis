using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class UserRecipient : EntityBase
    {
        public SystemUser SystemUser { get; set; }
        public int? SystemUserID { get; set; }
        public Recipient Recipient  { get; set; }
        public int? RecipientID { get; set; }
    }
}
