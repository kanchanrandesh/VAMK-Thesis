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
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpPost]
        [Route("search")]
        [HttpAuthorizeAccessRule(Rule = "EMPLYEVIEW")]
        public IHttpActionResult Search(EmployeeSearchQuery query)
        {
            if (query == null)
                query = new EmployeeSearchQuery();

            var returnList = new List<EmployeeModel>();
            foreach (var item in BizObjectFactory.GetEmployeeBO().Search(query))
                returnList.Add((EmployeeModel)item);

            return Ok(returnList);
        }

        [HttpGet]
        [Route("getById/{id}")]
        [HttpAuthorizeAccessRule(Rule = "EMPLYEVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var employee = new EmployeeModel();
            if (id != 0)
            {
                employee = (EmployeeModel)BizObjectFactory.GetEmployeeBO().GetSingle(id);
                var dbEmployee = BizObjectFactory.GetEmployeeBO().GetSingle(id);
                employee = (EmployeeModel)dbEmployee;

            }
            else
            {
                employee.isActive = true;
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "EMPLYEADED")]
        public IHttpActionResult Save(EmployeeModel model)
        {
            if (string.IsNullOrEmpty(model.code))
            {
                model.status = false;
                model.message = "Code needs to be entered";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.firstName))
            {
                model.status = false;
                model.message = "First Name needs to be entered";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.lastName))
            {
                model.status = false;
                model.message = "Last Name needs to be entered";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.userName))
            {
                model.status = false;
                model.message = "User Name needs to be entered";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.companyId))
            {
                model.status = false;
                model.message = "Company needs to be selected";
                return Ok(model);
            }
            if (string.IsNullOrEmpty(model.jobCategoryId))
            {
                model.status = false;
                model.message = "Job Category needs to be selected";
                return Ok(model);
            }

            if (string.IsNullOrEmpty(model.email))
            {
                model.status = false;
                model.message = "Email needs to be selected";
                return Ok(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Employee obj = model;


            Common.Envelop.TransferObject<Employee> transObject = null;
            if (obj.ID == null)
            {
                obj.State = State.Added;
                obj.DateCreated = DateTime.Now;
                obj.User = user.UserName;

                transObject = new EmployeeFacade(WebSettingProvider.GetWebSettings()).Save(obj);
            }
            else
            {
                var dbEmployee = BizObjectFactory.GetEmployeeBO().GetSingle(obj.ID.Value);
                dbEmployee.Code = obj.Code;
                dbEmployee.FirstName = obj.FirstName;
                dbEmployee.LastName = obj.LastName;
                dbEmployee.Phone = obj.Phone;

                dbEmployee.Mobile = obj.Mobile;
                dbEmployee.Email = obj.Email;
                dbEmployee.UserName = obj.UserName;
                dbEmployee.Password = obj.Password;
                dbEmployee.Company = null;
                dbEmployee.CompanyID = obj.CompanyID;
                dbEmployee.IsActive = obj.IsActive;
                dbEmployee.State = State.Modified;
                dbEmployee.DateModified = DateTime.Now;
                dbEmployee.User = user.UserName;
                dbEmployee.IsLocked = obj.IsLocked;
                dbEmployee.UnSuccessfulLoginAttempts = obj.UnSuccessfulLoginAttempts;
                dbEmployee.IsDoner = obj.IsDoner;
                dbEmployee.IsRecipient = obj.IsRecipient;
                dbEmployee.IsSalesEngineer = obj.IsSalesEngineer;
                dbEmployee.IsPreSaleEngineer = obj.IsPreSaleEngineer;
                dbEmployee.IsProjectManager = obj.IsProjectManager;
                dbEmployee.IsBizDeveloper = obj.IsBizDeveloper;
                dbEmployee.IsTechnicalPerson = obj.IsTechnicalPerson;
                dbEmployee.IsLeagalOfficer = obj.IsLeagalOfficer;

                transObject = new EmployeeFacade(WebSettingProvider.GetWebSettings()).Save(dbEmployee);
            }

            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            if (transObject.StatusInfo.Status == Common.Enums.ServiceStatus.DatabaseFailure)
                model.message = "Code / User name cannot be duplicated";
            else
                model.message = transObject.StatusInfo.Message;

            return Ok(model);
        }

        [HttpGet]
        [Route("getAll")]
        [HttpAuthorizeAccessRule(Rule = "EMPLYEVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<EmployeeModel>();
            foreach (var item in BizObjectFactory.GetEmployeeBO().GetAll())
                returnList.Add((EmployeeModel)item);

            return Ok(returnList.OrderBy(t => t.firstName).ToList());
        }




        [HttpPost]
        [Route("saveLockedEmployees")]
        [HttpAuthorizeAccessRule(Rule = "EMPLYEVIEW")]
        public IHttpActionResult SaveLockedEmployees(EmployeeModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            Employee obj = model;
            if (!obj.IsLocked)
                obj.UnSuccessfulLoginAttempts = 0;
            obj.State = State.Modified;
            obj.DateModified = DateTime.Now;
            obj.User = user.UserName;

            var transObject = new EmployeeFacade(WebSettingProvider.GetWebSettings()).SaveLockedEmployees(obj);
            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            model.message = transObject.StatusInfo.Message;
            return Ok(model);
        }
    }
}