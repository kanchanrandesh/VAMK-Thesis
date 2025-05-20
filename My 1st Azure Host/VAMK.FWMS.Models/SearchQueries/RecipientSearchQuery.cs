using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.SearchQueries
{
    public class RecipientSearchQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        // public string LocationCoordinates { get; set; }
        public string SearchText { get; set; }
    }
}
