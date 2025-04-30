using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.SearchQueries
{
    public class RequestSearchQuery
    {
        public DateTime? Date { get; set; }
        public string RequestStatus { get; set; }
        public string SearchText { get; set; }
    }
}
