using System.Collections.Generic;
using System.Web.Http;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Interfaces;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.WebSite.Models;
using VAMK.FWMS.WebSite.Filters;
using System.Security.Claims;
using System.Linq;
using VAMK.FWMS.WebSite.Helpers;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/dashboard")]
    [AuthorizeAccessRule(Rule = "DASHBDVIEW")]
    public class DashboardController : ApiController
    {
        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            SessionSetting session = new SessionSetting();
            int employeeId = session.LoginSession.UserID;

            var returnVal = new DashboardModel();
            returnVal.recentDonations = new List<DonationModel>();
            returnVal.recentRequests = new List<RequestModel>();

            var donationSearch = new DonationSearchQuery();
            //donationSearch.PayeeID = employeeId;
            var donations = BizObjectFactory.GetDonationBO().Search(donationSearch);
            //returnVal.openDonations = ious.Where(t => t.DonationSatus != VAMK.FWMS.Models.Enums.DonationSatus.ReadyToPickup).Count().ToString();
            returnVal.openDonations = donations.Where(t => t.DonationSatus == VAMK.FWMS.Models.Enums.DonationSatus.ReadyToPickup).Count().ToString();

            foreach (var item in donations.OrderByDescending(t => t.Date).ToList().Take(10))
                returnVal.recentDonations.Add((DonationModel)item);

            //donationSearch = new DonationSearchQuery();
            ////donationSearch.ApproverID = employeeId;
            //donations = BizObjectFactory.GetDonationBO().Search(donationSearch);
            //returnVal.totalRequestPostedToday = donations.Where(t => t.DonationSatus == VAMK.FWMS.Models.Enums.DonationSatus.ReadyToPickup).Count().ToString();

            donationSearch = new DonationSearchQuery();
            donations = BizObjectFactory.GetDonationBO().Search(donationSearch);
            returnVal.totalDonationsPostedToday = donations.Where(t => t.Date == System.DateTime.Now.Date).Count().ToString();


            donationSearch = new DonationSearchQuery();
            donations = BizObjectFactory.GetDonationBO().Search(donationSearch);
            returnVal.donationsCollectedToday = donations.Where(t => t.Date == System.DateTime.Now.Date&& t.DonationSatus== VAMK.FWMS.Models.Enums.DonationSatus.Collected).Count().ToString();

           


            var reqSearch = new RequestSearchQuery();
            //pcvSearch.PayeeID = employeeId;
            var reqs = BizObjectFactory.GetRequestBO().Search(reqSearch);
            returnVal.openRequests = reqs.Where(t => t.RequestStatus == VAMK.FWMS.Models.Enums.RequestStatus.AllocationPending).Count().ToString();

            foreach (var item in reqs.OrderByDescending(t => t.Date).ToList().Take(10))
                returnVal.recentRequests.Add((RequestModel)item);

            reqSearch = new RequestSearchQuery();
            //reqSearch.ApproverID = employeeId;
            reqs = BizObjectFactory.GetRequestBO().Search(reqSearch);
            returnVal.totalRequestPostedToday = reqs.Where(t => t.Date == System.DateTime.Now.Date).Count().ToString();


            reqSearch = new RequestSearchQuery();
            //reqSearch.ApproverID = employeeId;
            reqs = BizObjectFactory.GetRequestBO().Search(reqSearch);
            returnVal.requestsTobeIssued = reqs.Where(t => t.RequestStatus == VAMK.FWMS.Models.Enums.RequestStatus.IssuancePending).Count().ToString();

            reqSearch = new RequestSearchQuery();
            //reqSearch.ApproverID = employeeId;
            reqs = BizObjectFactory.GetRequestBO().Search(reqSearch);
            returnVal.requestsCompletedToday = reqs.Where(t => t.RequestStatus == VAMK.FWMS.Models.Enums.RequestStatus.Completed && t.Date == System.DateTime.Now.Date).Count().ToString();



            return Ok(returnVal);
        }
    }
}