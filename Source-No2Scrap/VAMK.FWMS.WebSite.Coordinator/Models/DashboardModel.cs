using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.Models
{
    public class DashboardModel
    {
        public string openDonations { get; set; }
        public string totalDonationsPostedToday { get; set; }
        public string donationsCollectedToday { get; set; }
        public string totalRequestPostedToday { get; set; }
        public string openRequests { get; set; }
        public string requestsTobeIssued { get; set; }
        public string requestsCompletedToday { get; set; }        
        public List<DonationModel> recentDonations { get; set; }
        public List<RequestModel> recentRequests { get; set; }
    }
}