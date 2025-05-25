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
    [RoutePrefix("api/request")]

    public class RequestController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "REQUESVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<RequestModel>();
            foreach (var item in BizObjectFactory.GetRequestBO().GetAll())
            {
                var modelObj = (RequestModel)item;
                returnList.Add(modelObj);
            }
            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "REQUESVIEW")]
        public IHttpActionResult Search(RequestSearchQuery query)
        {
            if (query == null)
                query = new RequestSearchQuery();

            var returnList = new List<RequestModel>();
            foreach (var item in BizObjectFactory.GetRequestBO().Search(query))
            {
                var modelObj = (RequestModel)item;
                returnList.Add(modelObj);
            }
           
            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "REQUESVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var request = new RequestModel();
            if (id != 0)
            {
                request = (RequestModel)BizObjectFactory.GetRequestBO().GetSingle(id);
                //foreach (var item in request.requestItemList)
                //{
                //    var stock = BizObjectFactory.GetInventoryStockBO().FindItemStock(int.Parse(item.itemID));
                //    if (stock != null)
                //    {
                //        if (decimal.Parse(item.requestedQty) <= stock.Quantity)
                //            item.allocatedQty = item.requestedQty;
                //        else
                //            item.allocatedQty = stock.Quantity.ToString();
                //    }
                //}
            }
            else
            {
                request.requestItemList = new List<RequestItemModel> { { new RequestItemModel { } } };
                request.transacionNumber = "-- AUTO GENERATED --";
                request.date = DateTime.Now.ToShortDateString();
            }
            return Ok(request);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "REQUESADED")]
        public IHttpActionResult Save(RequestModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            SequenceNumber sequenceNumber = null;
            var numberPrefix = new StringBuilder();
            numberPrefix.Append("REQ");

            var lastSequence = BizObjectFactory.GetSequenceNumberBO().GetLastNumber(VAMK.FWMS.Models.Enums.InventoryEffectedby.Request.ToString(), numberPrefix.ToString());
            numberPrefix.Append("-");
            numberPrefix.Append(DateTime.Now.Year.ToString());
            if (lastSequence == null)
            {
                model.transacionNumber = numberPrefix.ToString() + "-00001";

                sequenceNumber = new SequenceNumber();
                sequenceNumber.Type = VAMK.FWMS.Models.Enums.InventoryEffectedby.Request.ToString();
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

            Request obj = model;
            if (obj.ID == null)
            {
                obj.State = State.Added;
                obj.DateCreated = DateTime.Now;
                obj.DateIssued = null;
                obj.DateAccepted = null;

                foreach (var item in obj.RequestItemList)
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
                foreach (var item in obj.RequestItemList)
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

            var transObject = new RequestFacade().Save(obj, sequenceNumber);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
        [HttpPost]
        [Route("accept")]
        [HttpAuthorizeAccessRule(Rule = "REQUESADED")]
        public IHttpActionResult Accept(RequestModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Request obj = model;

            obj.State = State.Modified;
            obj.DateModified = DateTime.Now;
            foreach (var item in obj.RequestItemList)
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

            obj.User = user.UserName;
            obj.RequestStatus = FWMS.Models.Enums.RequestStatus.IssuancePending;
            obj.DateAccepted = DateTime.Now;
            
            var transObject = new RequestFacade().Save(obj, null);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [Route("issue")]
        [HttpAuthorizeAccessRule(Rule = "REQUESISSUE")]
        public IHttpActionResult Issue(RequestModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Request obj = model;

            obj.State = State.Modified;
            obj.DateModified = DateTime.Now;
            foreach (var item in obj.RequestItemList)
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

            obj.User = user.UserName;
            obj.RequestStatus = FWMS.Models.Enums.RequestStatus.Completed;
            obj.DateIssued = DateTime.Now;

            var transObject = new RequestFacade().Save(obj, null);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [Route("void")]
        [HttpAuthorizeAccessRule(Rule = "REQUESCANC")]
        public IHttpActionResult Cancel(RequestModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Request obj = model;

            obj.State = State.Modified;
            obj.DateModified = DateTime.Now;
            foreach (var item in obj.RequestItemList)
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

            obj.User = user.UserName;
            obj.RequestStatus = FWMS.Models.Enums.RequestStatus.Cancelled;
            obj.DateIssued = DateTime.Now;

            var transObject = new RequestFacade().Save(obj, null);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}