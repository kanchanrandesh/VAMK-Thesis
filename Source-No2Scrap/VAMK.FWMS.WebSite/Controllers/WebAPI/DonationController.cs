using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Interfaces;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.WebSite.Filters;
using VAMK.FWMS.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using VAMK.FWMS.WebSite.Helpers;
using System.Linq;
using System.Text;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/donation")]

    public class DonationController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<DonationModel>();
            foreach (var item in BizObjectFactory.GetDonationBO().GetAll())
            {
                var modelObj = (DonationModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult Search(DonationSearchQuery query)
        {
            if (query == null)
                query = new DonationSearchQuery();

            var returnList = new List<DonationModel>();
            foreach (var item in BizObjectFactory.GetDonationBO().Search(query))
            {
                var modelObj = (DonationModel)item;
                returnList.Add(modelObj);
            }
            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var donation = new DonationModel();
            if (id != 0)
                donation = (DonationModel)BizObjectFactory.GetDonationBO().GetSingle(id);
            else
                donation.donationItemList = new List<DonationItemModel> { { new DonationItemModel { } } };

            return Ok(donation);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMADED")]
        public IHttpActionResult Save(DonationModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            SequenceNumber sequenceNumber = null;
            var numberPrefix = new StringBuilder();
            numberPrefix.Append("DON");


            var lastSequence = BizObjectFactory.GetSequenceNumberBO().GetLastNumber(VAMK.FWMS.Models.Enums.InventoryEffectedby.Donation.ToString(), numberPrefix.ToString());
            numberPrefix.Append("-");
            numberPrefix.Append(DateTime.Now.Year.ToString());
            if (lastSequence == null)
            {
                model.transacionNumber = numberPrefix.ToString() + "-00001";

                sequenceNumber = new SequenceNumber();
                sequenceNumber.Type = VAMK.FWMS.Models.Enums.InventoryEffectedby.Donation.ToString();
                sequenceNumber.Prefix = numberPrefix.ToString();
                sequenceNumber.LastNumber = 1;
                sequenceNumber.State = State.Added;
                sequenceNumber.DateCreated = DateTime.Now;
                sequenceNumber.User = user.UserName;
            }
            else
            {
                var nextNumber = lastSequence.LastNumber + 1;
                model.transacionNumber = numberPrefix.ToString() + "-" + nextNumber.ToString().PadLeft(5, '0');

                sequenceNumber = lastSequence;
                sequenceNumber.LastNumber = nextNumber;
                sequenceNumber.State = State.Modified;
                sequenceNumber.DateCreated = DateTime.Now;
                sequenceNumber.User = user.UserName;

            }


            Donation obj = model;
            if (obj.ID == null)
            {
                obj.State = State.Added;
                obj.DateCreated = DateTime.Now;
                foreach (var item in obj.DonationItemList)
                {
                    if (item.ID == null)
                    {
                        item.State = State.Added;
                        item.DateCreated = DateTime.Now;
                    }
                    item.User = user.UserName;
                }
            }
            else
            {
                obj.State = State.Modified;
                obj.DateModified = DateTime.Now;
                foreach (var item in obj.DonationItemList)
                {
                    if (item.ID == null)
                    {
                        item.State = State.Added;
                        item.DateCreated = DateTime.Now;
                    }
                    else
                    {
                        item.State = State.Modified;
                        item.DateCreated = DateTime.Now;
                    }
                    item.User = user.UserName;
                }
            }
            obj.User = user.UserName;

            var transObject = new DonationFacade().Save(obj, sequenceNumber);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}