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

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/recipient")]

    public class RecipientController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "RECPTVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<RecipientModel>();
            foreach (var item in BizObjectFactory.GetRecipientBO().GetAll())
            {
                var modelObj = (RecipientModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "RECPTVIEW")]
        public IHttpActionResult Search(RecipientSearchQuery query)
        {
            if (query == null)
                query = new RecipientSearchQuery();

            var returnList = new List<RecipientModel>();
            foreach (var item in BizObjectFactory.GetRecipientBO().Search(query))
            {
                var modelObj = (RecipientModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "RECPTVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var recipient = new RecipientModel();
            if (id != 0)
                recipient = (RecipientModel)BizObjectFactory.GetRecipientBO().GetSingle(id);
            else
                recipient.contactPersonList = new List<ContactPersonModel> { { new ContactPersonModel { } } };

            return Ok(recipient);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "RECPTADED")]
        public IHttpActionResult Save(RecipientModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Recipient obj = model;
            if (obj.ID == null)
            {
                obj.State = State.Added;
                obj.DateCreated = DateTime.Now;
            }
            else
            {
                obj.State = State.Modified;
                obj.DateModified = DateTime.Now;
            }
            obj.User = user.UserName;

            var transObject = new RecipientFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
        [HttpGet]
        [Route("getAllForDropdown")]
        public IHttpActionResult GetAllForDropdown()
        {
            var returnList = new List<SelectObjectModel>();
            returnList.Add(new SelectObjectModel { id = "-", name = "-- Select --" });
            foreach (var item in BizObjectFactory.GetRecipientBO().GetAll())
                returnList.Add(new SelectObjectModel { id = item.ID.Value.ToString(), name = item.Code + " - " + item.Name });

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getAllForDropdownForUser")]
        [HttpAuthorizeAccessRule(Rule = "RECPTVIEW")]
        public IHttpActionResult GetAllForDropdownForUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetSingle(int.Parse(userid));
            var returnList = new List<SelectObjectModel>();

            returnList.Add(new SelectObjectModel { id = "-", name = "-- Select --" });
            foreach (var item in user.EmployeeRecipients)
            {
                item.Recipient = BizObjectFactory.GetRecipientBO().GetSingle(item.RecipientID.Value);
                returnList.Add(new SelectObjectModel { id = item.RecipientID.Value.ToString(), name = item.Recipient.Code + " - " + item.Recipient.Name });
            }

            return Ok(returnList);
        }
    }
}