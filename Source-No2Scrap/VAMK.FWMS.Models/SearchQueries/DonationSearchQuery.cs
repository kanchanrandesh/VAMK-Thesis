using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.Models.SearchQueries
{
    public class DonationSearchQuery
    {
        public string ManualRefNumber { get; set; }
        public string TransacionNumber { get; set; }
        public Doner Doner { get; set; }
        public int? DonerID { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public Models.Enums.DonationSatus DonationSatus { get; set; }
        public string SearchText { get; set; }
    }
}
