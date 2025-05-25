using System.Collections.Generic;
using System.Web.Http;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Interfaces;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.WebSite.Models;
using System.Linq;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Filters;
using System.Security.Claims;
using System;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/group")]
    public class GroupController : ApiController
    {
        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "USGROPVIEW")]
        public IHttpActionResult Search(GroupSearchQuery query)
        {
            if (query == null)
                query = new GroupSearchQuery();

            var returnList = new List<GroupModel>();
            foreach (var item in BizObjectFactory.GetGroupBO().Search(query))
                returnList.Add((GroupModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "USGROPVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var group = new GroupModel();
            if (id != 0)
                group = (GroupModel)BizObjectFactory.GetGroupBO().GetSingle(id);

            return Ok(group);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "USGROPADED")]
        public IHttpActionResult Save(GroupModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Group obj = model;
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

            var transObject = new GroupFacade().Save(obj);

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Description cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "USGROPVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<GroupModel>();
            foreach (var item in BizObjectFactory.GetGroupBO().GetAll())
                returnList.Add((GroupModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getForGroupUsers/{id}")]
        [HttpAuthorizeAccessRule(Rule = "USGROPVIEW")]
        public IHttpActionResult GetForGroupUsers(int id)
        {
            var groupModel = new GroupModel();
            var dbGroup = BizObjectFactory.GetGroupBO().GetSingle(id);
            groupModel = (GroupModel)dbGroup;
            groupModel.assignedList = new List<SelectObjectModel>();
            groupModel.notAssignedList = new List<SelectObjectModel>();

            var employees = BizObjectFactory.GetEmployeeBO().GetAll();
            var groupUsers = BizObjectFactory.GetGroupEmployeeBO().GetAllFor(dbGroup);
            var employeeIDList = new List<int>();
            foreach (var item in groupUsers)
            {
                employeeIDList.Add(item.EmployeeID.Value);
                var employee = employees.FirstOrDefault(t => t.ID == item.EmployeeID);

                var selectObject = new SelectObjectModel();
                selectObject.id = item.EmployeeID.Value.ToString();
                selectObject.name = employee.FirstName + " " + employee.LastName;
                groupModel.assignedList.Add(selectObject);
            }

            var notSelectedList = employees.Where(t => !employeeIDList.Contains(t.ID.Value));
            foreach (var item in notSelectedList)
            {
                var selectObject = new SelectObjectModel();
                selectObject.id = item.ID.Value.ToString();
                selectObject.name = item.FirstName + " " + item.LastName;
                groupModel.notAssignedList.Add(selectObject);
            }

            return Ok(groupModel);
        }

        [HttpPost]
        [Route("saveUsers")]
        [HttpAuthorizeAccessRule(Rule = "USGROPADED")]
        public IHttpActionResult SaveUsers(GroupModel model)
        {
            Group obj = model;
            var dbGroupEmployees = BizObjectFactory.GetGroupEmployeeBO().GetAllFor(obj);

            var newList = new List<GroupUser>();
            foreach (var item in model.assignedList)
            {
                var groupEmployee = dbGroupEmployees.FirstOrDefault(t => t.EmployeeID.Value.ToString() == item.id);
                if (groupEmployee == null)
                {
                    groupEmployee = new GroupUser();
                    groupEmployee.GroupID = obj.ID;
                    groupEmployee.EmployeeID = Utility.ParseInt(item.id);
                    groupEmployee.State = State.Added;
                    newList.Add(groupEmployee);
                }
            }

            List<GroupUser> deleteList = dbGroupEmployees.Where(p => model.assignedList.All(p2 => p2.id != p.EmployeeID.Value.ToString() && p.EmployeeID.Value.ToString() != string.Empty)).ToList();
            foreach (var item in deleteList)
                item.State = State.Deleted;

            var transObject = new GroupFacade().SaveUsers(newList, deleteList);
            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [HttpGet]
        [Route("getForGroupRules/{id}")]
        [HttpAuthorizeAccessRule(Rule = "USGROPVIEW")]
        public IHttpActionResult GetForGroupRules(int id)
        {
            var groupModel = new GroupModel();
            var dbGroup = BizObjectFactory.GetGroupBO().GetSingle(id);
            groupModel = (GroupModel)dbGroup;
            groupModel.assignedList = new List<SelectObjectModel>();
            groupModel.notAssignedList = new List<SelectObjectModel>();

            var rules = BizObjectFactory.GetRuleBO().GetAll();
            var groupRules = BizObjectFactory.GetGroupRuleBO().GetAllFor(dbGroup);
            var ruleIDList = new List<int>();
            foreach (var item in groupRules)
            {
                ruleIDList.Add(item.RuleID.Value);
                var rule = rules.FirstOrDefault(t => t.ID == item.RuleID);

                var selectObject = new SelectObjectModel();
                selectObject.id = item.RuleID.Value.ToString();
                selectObject.name = rule.Description;
                groupModel.assignedList.Add(selectObject);
            }

            var notSelectedList = rules.Where(t => !ruleIDList.Contains(t.ID.Value));
            foreach (var item in notSelectedList)
            {
                var selectObject = new SelectObjectModel();
                selectObject.id = item.ID.Value.ToString();
                selectObject.name = item.Description;
                groupModel.notAssignedList.Add(selectObject);
            }

            return Ok(groupModel);
        }

        [HttpPost]
        [Route("saveRules")]
        [HttpAuthorizeAccessRule(Rule = "USGROPADED")]
        public IHttpActionResult SaveRules(GroupModel model)
        {
            Group obj = model;
            var dbGroupRules = BizObjectFactory.GetGroupRuleBO().GetAllFor(obj);

            var newList = new List<GroupRule>();
            foreach (var item in model.assignedList)
            {
                var groupRule = dbGroupRules.FirstOrDefault(t => t.RuleID.Value.ToString() == item.id);
                if (groupRule == null)
                {
                    groupRule = new GroupRule();
                    groupRule.GroupID = obj.ID;
                    groupRule.RuleID = Utility.ParseInt(item.id);
                    groupRule.State = State.Added;
                    newList.Add(groupRule);
                }
            }

            List<GroupRule> deleteList = dbGroupRules.Where(p => model.assignedList.All(p2 => p2.id != p.RuleID.Value.ToString() && p.RuleID.Value.ToString() != string.Empty)).ToList();
            foreach (var item in deleteList)
                item.State = State.Deleted;

            var transObject = new GroupFacade().SaveRules(newList, deleteList);
            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }
    }
}