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
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        //[HttpAuthorizeAccessRule(Rule = "CONTRYVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<CountryModel>();
            foreach (var item in BizObjectFactory.GetCountryBO().GetAll())
                returnList.Add((CountryModel)item);

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "CONTRYVIEW")]
        public IHttpActionResult Search(CountrySearchQuery query)
        {
            if (query == null)
                query = new CountrySearchQuery();

            var returnList = new List<CountryModel>();
            foreach (var item in BizObjectFactory.GetCountryBO().Search(query))
                returnList.Add((CountryModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "CONTRYVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var country = new CountryModel();
            if (id != 0)
                country = (CountryModel)BizObjectFactory.GetCountryBO().GetSingle(id);

            return Ok(country);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "CONTRYADED")]
        public IHttpActionResult Save(CountryModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Country obj = model;
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

            var transObject = new CountryFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Name cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}