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
using System;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/unit")]
    public class UnitController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "UNITVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<UnitModel>();
            foreach (var item in BizObjectFactory.GetUnitBO().GetAll())
            {
                var modelObj = (UnitModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "UNITVIEW")]
        public IHttpActionResult Search(UnitSearchQuery query)
        {
            if (query == null)
                query = new UnitSearchQuery();

            var returnList = new List<UnitModel>();
            foreach (var item in BizObjectFactory.GetUnitBO().Search(query))
            {
                var modelObj = (UnitModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "UNITVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var unit = new UnitModel();
            if (id != 0)
                unit = (UnitModel)BizObjectFactory.GetUnitBO().GetSingle(id);

            return Ok(unit);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "UNITADED")]
        public IHttpActionResult Save(UnitModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Unit obj = model;
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

            var transObject = new UnitFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}