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
        [HttpAuthorizeAccessRule(Rule = "UNITVIEWFN")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<UnitOfMeasurementModel>();
            foreach (var item in BizObjectFactory.GetUnitBO().GetAll())
            {
                var modelObj = (UnitOfMeasurementModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "UNITVIEWFN")]
        public IHttpActionResult Search(UnitSearchQuery query)
        {
            if (query == null)
                query = new UnitSearchQuery();

            var returnList = new List<UnitOfMeasurementModel>();
            foreach (var item in BizObjectFactory.GetUnitBO().Search(query))
            {
                var modelObj = (UnitOfMeasurementModel)item;
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "UNITVIEWFN")]
        public IHttpActionResult GetById(int id)
        {
            var unit = new UnitOfMeasurementModel();
            if (id != 0)
                unit = (UnitOfMeasurementModel)BizObjectFactory.GetUnitBO().GetSingle(id);

            return Ok(unit);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "UNITADDEDT")]
        public IHttpActionResult Save(UnitOfMeasurementModel model)
        {
            if (string.IsNullOrEmpty(model.departmentId))
            {
                model.status = false;
                model.message = "Department needs to be selected";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.authorizedOfficerId))
            {
                model.status = false;
                model.message = "Authorized Officer needs to be selected";
                return Ok(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            UnitOfMeasurement obj = model;
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

            var transObject = new UnitOfMeasurementFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}