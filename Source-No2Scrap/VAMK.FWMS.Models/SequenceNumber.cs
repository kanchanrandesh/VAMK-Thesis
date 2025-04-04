using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models
{
    public class SequenceNumber : EntityBase
    {
        public string Type { get; set; }
        public string Prefix { get; set; }
        public int LastNumber { get; set; }
    }
}
