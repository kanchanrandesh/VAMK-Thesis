using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.Models
{
    public class Item : EntityBase
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
        public Models.Enums.ItemCategory ItemCategory { get; set; }
        public Unit Unit { get; set; }
        public int? UnitID { get; set; }

    }
}