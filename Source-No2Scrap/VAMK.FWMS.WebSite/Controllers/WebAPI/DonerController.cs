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
    [RoutePrefix("api/doner")]

    public class DonerController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "DONERVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<DonerModel>();
            foreach (var item in BizObjectFactory.GetDonerBO().GetAll())
            {
                var modelObj = (DonerModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "DONERVIEW")]
        public IHttpActionResult Search(DonerSearchQuery query)
        {
            if (query == null)
                query = new DonerSearchQuery();

            var returnList = new List<DonerModel>();
            foreach (var item in BizObjectFactory.GetDonerBO().Search(query))
            {
                var modelObj = (DonerModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "DONERVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var doner = new DonerModel();
            if (id != 0)
                doner = (DonerModel)BizObjectFactory.GetDonerBO().GetSingle(id);
            else
                doner.contactPersonList = new List<ContactPersonModel> { { new ContactPersonModel { } } };
            //doner.contactPersonList = new List<ContactPersonModel>();

            return Ok(doner);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "DONERADED")]
        public IHttpActionResult Save(DonerModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Doner obj = model;
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

            var transObject = new DonerFacade().Save(obj);

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
            foreach (var item in BizObjectFactory.GetDonerBO().GetAll())
                returnList.Add(new SelectObjectModel { id = item.ID.Value.ToString(), name = item.Code + " - " + item.Name });

            return Ok(returnList);
        }


        [HttpGet]
        [Route("getAllForDropdownForUser")]
        [HttpAuthorizeAccessRule(Rule = "DONERVIEW")]
        public IHttpActionResult GetAllForDropdownForUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetSingle(int.Parse(userid));
            var returnList = new List<SelectObjectModel>();

            returnList.Add(new SelectObjectModel { id = "-", name = "-- Select --" });
            foreach (var item in user.EmployeeDoners)
            {
                item.Doner = BizObjectFactory.GetDonerBO().GetSingle(item.DonerID.Value);
                returnList.Add(new SelectObjectModel { id = item.DonerID.Value.ToString(), name = item.Doner.Code + " - " + item.Doner.Name });
            }

            return Ok(returnList);
        }
    }
}