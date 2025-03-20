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

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/doner")]

    public class DonerController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
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
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
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
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var department = new DonerModel();
            if (id != 0)
                department = (DonerModel)BizObjectFactory.GetDonerBO().GetSingle(id);

            return Ok(department);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMADED")]
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
    }
}