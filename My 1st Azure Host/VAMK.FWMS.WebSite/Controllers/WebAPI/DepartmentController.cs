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
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<DepartmentModel>();
            foreach (var item in BizObjectFactory.GetDepartmentBO().GetAll())
            {
                var modelObj = (DepartmentModel)item;
                if (item.AuthorizedOfficerID != null)
                {
                    item.AuthorizedOfficer = BizObjectFactory.GetEmployeeBO().GetProxy(item.AuthorizedOfficerID.Value);
                    modelObj.authorizedOfficerName = item.AuthorizedOfficer.FirstName + " " + item.AuthorizedOfficer.LastName;
                }
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult Search(DepartmentSearchQuery query)
        {
            if (query == null)
                query = new DepartmentSearchQuery();

            var returnList = new List<DepartmentModel>();
            foreach (var item in BizObjectFactory.GetDepartmentBO().Search(query))
            {
                var modelObj = (DepartmentModel)item;
                if (item.AuthorizedOfficerID != null)
                {
                    item.AuthorizedOfficer = BizObjectFactory.GetEmployeeBO().GetProxy(item.AuthorizedOfficerID.Value);
                    modelObj.authorizedOfficerName = item.AuthorizedOfficer.FirstName + " " + item.AuthorizedOfficer.LastName;
                }
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var department = new DepartmentModel();
            if (id != 0)
                department = (DepartmentModel)BizObjectFactory.GetDepartmentBO().GetSingle(id);

            return Ok(department);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMADED")]
        public IHttpActionResult Save(DepartmentModel model)
        {
            if (string.IsNullOrEmpty(model.authorizedOfficerId))
            {
                model.status = false;
                model.message = "Authorized Officer needs to be selected";
                return Ok(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Department obj = model;
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

            var transObject = new DepartmentFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}