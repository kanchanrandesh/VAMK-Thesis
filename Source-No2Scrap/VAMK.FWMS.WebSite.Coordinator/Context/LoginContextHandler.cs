using VAMK.FWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAMK.FWMS.WebSite.NewFolder1
{
    public class LoginContextHandler
    {
        public OperationStatus CheckAuthorizationRule(VAMK.FWMS.Models.SystemUser employee, string ruleCode)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = "Fail";
            if (employee == null || ruleCode == string.Empty)
            {
                operationStatus.Status = "Fail";
                operationStatus.Message = "Error Messeage";//TODO:Get the error code from resource file.
            }
            VAMK.FWMS.Common.Envelop.TransferObject<bool> transferObject = new VAMK.FWMS.BizObjects.Auth.LoginService().IsRuleCodeAllowed(employee.UserName, ruleCode);
            if (transferObject != null)
            {
                operationStatus.Status = transferObject.StatusInfo.Status.ToString();
                operationStatus.Message = transferObject.StatusInfo.Message;
            }
            return operationStatus;

        }
    }
}