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
    [AuthorizeAccessRule(Rule = "GENERALACC")]
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
            //returnVal.recentIOUs = new List<IOUModel>();
            //returnVal.recentPcvs = new List<PettyCashVoucherModel>();
            //returnVal.recentMCs = new List<MedicalClaimModel>();

            //var iouSearch = new IOUSearchQuery();
            //iouSearch.PayeeID = employeeId;
            //var ious = BizObjectFactory.GetIOUBO().Search(iouSearch);
            //returnVal.openIOUs = ious.Where(t => t.Status != VAMK.FWMS.Models.Enums.IOUStatus.Settled).Count().ToString();

            //foreach (var item in ious.OrderByDescending(t => t.RequestedDate).ToList().Take(10))
            //    returnVal.recentIOUs.Add((IOUModel)item);

            //iouSearch = new IOUSearchQuery();
            //iouSearch.ApproverID = employeeId;
            //ious = BizObjectFactory.GetIOUBO().SearchForIOUApproval(iouSearch);
            //returnVal.iousToApprove = ious.Count().ToString();

            //var pcvSearch = new PettyCashVoucherSearchQuery();
            //pcvSearch.PayeeID = employeeId;
            //var pcvs = BizObjectFactory.GetPettyCashVoucherBO().Search(pcvSearch);
            //returnVal.openPCVs = pcvs.Where(t => t.Status != VAMK.FWMS.Models.Enums.PettyCashVoucherStatus.Disbursed && t.Status != VAMK.FWMS.Models.Enums.PettyCashVoucherStatus.Cancelled).Count().ToString();

            //foreach (var item in pcvs.OrderByDescending(t => t.RequestedDate).ToList().Take(10))
            //    returnVal.recentPcvs.Add((PettyCashVoucherModel)item);

            //pcvSearch = new PettyCashVoucherSearchQuery();
            //pcvSearch.ApproverID = employeeId;
            //pcvs = BizObjectFactory.GetPettyCashVoucherBO().SearchForPettyCashApproval(pcvSearch);
            //returnVal.pcvsToApprove = pcvs.Count().ToString();

            //var mcSearch = new MedicalClaimSearchQuery();
            //mcSearch.PayeeID = employeeId;
            //mcSearch.Type = "Open";
            //var mcs = BizObjectFactory.GetMedicalClaimBO().Search(mcSearch);
            //returnVal.openMCs = mcs.Count().ToString();

            //foreach (var item in mcs.OrderByDescending(t => t.RequestedDate).ToList().Take(10))
            //    returnVal.recentMCs.Add((MedicalClaimModel)item);

            //mcSearch = new MedicalClaimSearchQuery();
            //mcSearch.ApproverID = employeeId;
            //mcs = BizObjectFactory.GetMedicalClaimBO().SearchForMedicalClaimApproval(mcSearch);
            //returnVal.mcsToApprove = mcs.Count().ToString();
            return Ok(returnVal);
        }
    }
}