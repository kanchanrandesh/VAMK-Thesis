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
        [HttpAuthorizeAccessRule(Rule = "USERVIEW")]
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
        [HttpAuthorizeAccessRule(Rule = "USERVIEW")]
        public IHttpActionResult GetById(int id)
        {
            var employee = new EmployeeModel();
            if (id != 0)
            {
                employee = (EmployeeModel)BizObjectFactory.GetEmployeeBO().GetSingle(id);
                var dbEmployee = BizObjectFactory.GetEmployeeBO().GetSingle(id);
                foreach (var employeeDoner in dbEmployee.UserDoners)
                {
                    if (employeeDoner.DonerID != null)
                        employeeDoner.Doner = BizObjectFactory.GetDonerBO().GetSingle(employeeDoner.DonerID.Value);
                }
                foreach (var employeeRecipient in dbEmployee.UserRecipients)
                {
                    if (employeeRecipient.RecipientID != null)
                        employeeRecipient.Recipient = BizObjectFactory.GetRecipientBO().GetSingle(employeeRecipient.RecipientID.Value);
                }
                employee = (EmployeeModel)dbEmployee;

            }
            else
            {
                employee.isActive = true;
                employee.employeeDoners = new List<EmployeeDonerModel>();
                employee.employeeRecipients = new List<EmployeeRecipientModel>();
            }
            return Ok(employee);
        }

        [HttpPost]
        [Route("save")]
        [HttpAuthorizeAccessRule(Rule = "USERADED")]
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

            //if (string.IsNullOrEmpty(model.companyId))
            //{
            //    model.status = false;
            //    model.message = "Company needs to be selected";
            //    return Ok(model);
            //}
            //if (string.IsNullOrEmpty(model.jobCategoryId))
            //{
            //    model.status = false;
            //    model.message = "Job Category needs to be selected";
            //    return Ok(model);
            //}

            if (string.IsNullOrEmpty(model.email))
            {
                model.status = false;
                model.message = "Email needs to be selected";
                return Ok(model);
            }

            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            SystemUser obj = model;


            Common.Envelop.TransferObject<SystemUser> transObject = null;
            if (obj.ID == null)
            {
                obj.State = State.Added;
                obj.DateCreated = DateTime.Now;
                obj.UserName = user.UserName;

                transObject = new UserFacade(WebSettingProvider.GetWebSettings()).Save(obj);
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
                dbEmployee.IsActive = obj.IsActive;
                dbEmployee.State = State.Modified;
                dbEmployee.DateModified = DateTime.Now;
                dbEmployee.UserName = user.UserName;
                dbEmployee.IsLocked = obj.IsLocked;
                dbEmployee.UnSuccessfulLoginAttempts = obj.UnSuccessfulLoginAttempts;
                dbEmployee.IsDoner = obj.IsDoner;
                dbEmployee.IsRecipient = obj.IsRecipient;              
                dbEmployee.UserRecipients = obj.UserRecipients;
                dbEmployee.UserDoners = obj.UserDoners;


                transObject = new UserFacade(WebSettingProvider.GetWebSettings()).Save(dbEmployee);
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
        [HttpAuthorizeAccessRule(Rule = "USERVIEW")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<EmployeeModel>();
            foreach (var item in BizObjectFactory.GetEmployeeBO().GetAll())
                returnList.Add((EmployeeModel)item);

            return Ok(returnList.OrderBy(t => t.firstName).ToList());
        }

        [HttpPost]
        [Route("saveLockedEmployees")]
        [HttpAuthorizeAccessRule(Rule = "USERVIEW")]
        public IHttpActionResult SaveLockedEmployees(EmployeeModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userid = identity.FindFirst(ClaimTypes.Sid).Value.ToString();
            var user = BizObjectFactory.GetEmployeeBO().GetProxy(int.Parse(userid));

            SystemUser obj = model;
            if (!obj.IsLocked)
                obj.UnSuccessfulLoginAttempts = 0;
            obj.State = State.Modified;
            obj.DateModified = DateTime.Now;
            obj.UserName = user.UserName;

            var transObject = new UserFacade(WebSettingProvider.GetWebSettings()).SaveLockedEmployees(obj);
            model.status = transObject.StatusInfo.Status == Common.Enums.ServiceStatus.Success;
            model.message = transObject.StatusInfo.Message;
            return Ok(model);
        }
    }
}