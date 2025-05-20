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
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "COMPNYVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<CompanyModel>();
            foreach (var item in BizObjectFactory.GetCompanyBO().GetAll())
                returnList.Add((CompanyModel)item);

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "COMPNYVIEW")]
        public IHttpActionResult Search(CompanySearchQuery query)
        {
            if (query == null)
                query = new CompanySearchQuery();

            var returnList = new List<CompanyModel>();
            foreach (var item in BizObjectFactory.GetCompanyBO().Search(query))
                returnList.Add((CompanyModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "COMPNYVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var company = new CompanyModel();
            if (id != 0)
                company = (CompanyModel)BizObjectFactory.GetCompanyBO().GetSingle(id);

            return Ok(company);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "COMPNYADED")]
        public IHttpActionResult Save(CompanyModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Company obj = model;
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

            var transObject = new CompanyFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Name cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}