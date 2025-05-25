using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.DataObjects;
using VAMK.FWMS.DataObjects.Interfaces;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects.Auth
{
    public class LoginService //: ILoginService
    {
        IEmployeeRepository _employeeRepository = null;
        #region - Costructor -

        public LoginService() : this(new EmployeeRepository()) { }
        public LoginService(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
        }

        #endregion

        /// <summary>
        /// This is used to validate rule code
        /// </summary>
        /// <param name="User"></param>
        /// <param name="RuleCode"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Common.Envelop.TransferObject<bool> IsRuleCodeAllowed(string User, string RuleCode)
        {
            Common.Envelop.TransferObject<bool> _transferObject = new TransferObject<bool>();
            _transferObject.StatusInfo.Message = Resources.MessageDictionary.USER_AUTHORIZATION_ERROR;//HACK:Get using resource file..
            _transferObject.ReturnValue = false;

            SystemUser employee = null;
            employee = _employeeRepository.GetEmployeeByUserName(User);
            if (employee == null)
            {
                _transferObject.ReturnValue = false;
                //_transferObject.StatusInfo.Message = "Employee not found";//TODO:
                _transferObject.StatusInfo.Message = Resources.MessageDictionary.USER_INVALID_ERROR;//HACK:Get using resource file..
                _transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                return _transferObject;
            }

            //Groups for the employee
            Models.SearchQueries.EmployeeSearchQuery _searchQuery = new Models.SearchQueries.EmployeeSearchQuery();
            //_searchQuery.IncludeProperties.Add("Group");
            //_searchQuery.Employee = employee;
            //_searchQuery.DomainID = domainId;
            IList<VAMK.FWMS.Models.SystemUser> groupEmployeeList = VAMK.FWMS.BizObjects.BizObjectFactory.GetEmployeeBO().Search(_searchQuery);
            if (groupEmployeeList == null)
            {
                _transferObject.ReturnValue = false;
                //_transferObject.StatusInfo.Message = "No Groups assign for this user";//TODO:
                _transferObject.StatusInfo.Message = Resources.MessageDictionary.USER_GROUPNOTFOUND_ERROR;//HACK:Get using resource file..
                _transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                return _transferObject;
            }

            ////Check the Rule Code...
            //Models.Rule rule = VAMK.FWMS.BizObjects.BizObjectFactory.GetRuleBO().Search(new Models.SearchQueries.RuleSearchQuery { DomainID = domainId, Text = RuleCode }).FirstOrDefault(t => t.Code == RuleCode);
            //if (rule == null)
            //{
            //    _transferObject.ReturnValue = false;
            //    //_transferObject.StatusInfo.Message = string.Format("Rule {0} is not defined", RuleCode);//TODO:
            //    _transferObject.StatusInfo.Message = Resources.MessageDictionary.USER_RULENOTDEFINE_ERROR;//HACK:Get using resource file..
            //    _transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
            //    return _transferObject;
            //}

            //foreach (var item in groupEmployeeList)
            //{
            //    var groupRuleList = VAMK.FWMS.BizObjects.BizObjectFactory.GetGroupRuleBO().Search(new Models.SearchQueries.GroupRuleSearchQuery { DomainID = domainId, Group = item.Group, Rule = rule });
            //    if (groupRuleList != null && groupRuleList.Count > 0) //Record found
            //    {
            //        _transferObject.ReturnValue = true;
            //        _transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.Success;
            //        return _transferObject;
            //    }

            //}
            return _transferObject;
        }
    }
}
