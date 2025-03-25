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
    [RoutePrefix("api/item")]

    public class ItemController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<ItemModel>();
            foreach (var item in BizObjectFactory.GetItemBO().GetAll())
            {
                var modelObj = (ItemModel)item;                
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        public IHttpActionResult Search(ItemSearchQuery query)
        {
            if (query == null)
                query = new ItemSearchQuery();

            var returnList = new List<ItemModel>();
            foreach (var item in BizObjectFactory.GetItemBO().Search(query))
            {
                var modelObj = (ItemModel)item;                
                returnList.Add(modelObj);
            }

            return Ok(returnList);
        }

        [HttpGet]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMVIEW")]
        [Route("getById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var item = new ItemModel();
            if (id != 0)
                item = (ItemModel)BizObjectFactory.GetItemBO().GetSingle(id);

            return Ok(item);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "DEPRTMADED")]
        public IHttpActionResult Save(ItemModel model)
        {            
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Item obj = model;
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

            var transObject = new ItemFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}