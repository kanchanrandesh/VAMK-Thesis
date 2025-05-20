using System.Collections.Generic;
using System.Web.Http;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.BizObjects;
using VAMK.FWMS.Models.Interfaces;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.WebSite.Models;
using System.Linq;
using VAMK.FWMS.WebSite.Filters;
using System.Security.Claims;
using System;
using System.Text;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/ContactPerson")]
    public class ContactPersonController : ApiController
    {
        

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "CONTCTADED")]
        public IHttpActionResult GetById(int id)
        {
            var ContactPerson = new ContactPersonModel();
            if (id != 0)
                ContactPerson = (ContactPersonModel)BizObjectFactory.GetContactPersonBO().GetSingle(id);

            return Ok(ContactPerson);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "CONTCTADED")]
        public IHttpActionResult Save(ContactPersonModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            ContactPerson obj = model;
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

            var transObject = new ContactPersonFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "CONTCTVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<ContactPersonModel>();
            foreach (var item in BizObjectFactory.GetContactPersonBO().GetAll())
                returnList.Add((ContactPersonModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("export")]
        [HttpAuthorizeAccessRule(Rule = "CONTCTVIEW")]
        public IHttpActionResult Export()
        {
            return Ok(new ContactPersonFacade().ExportToExcel());
        }
    }
}