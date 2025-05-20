using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Common.Utility;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Enums;
using VAMK.FWMS.Models.MessageModels;
using VAMK.FWMS.Models.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class EmployeeFacade
    {
        WebSettings websettings;

        public EmployeeFacade(WebSettings websettings)
        {
            this.websettings = websettings;
        }

        public TransferObject<Employee> Save(Employee employee)
        {
            EmailOutBox emailOutBox = null;
            var transferObject = new TransferObject<Employee>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            bool isNewEmployee = false;

            AuditTrail auditTrail = null;
            if (employee.ID == null)
            {
                CryptoProvider crypto = new CryptoProvider();

                string tempPassword = System.Web.Security.Membership.GeneratePassword(6, 1);
                employee.Password = tempPassword;
                emailOutBox = GenerateEmailItem(employee, websettings.FunctionURLs);

                employee.Password = crypto.GetHash(tempPassword);
                isNewEmployee = true;

                auditTrail = Resources.Utility.CreateAuditTrail(null, employee, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            }
            else
            {
                var dbEmployee = BizObjectFactory.GetEmployeeBO().GetSingle(employee.ID.Value);

                var childListNames = new List<string>();
                childListNames.Add("EmployeeDoners");
                childListNames.Add("EmployeeRecipients");
                auditTrail = Resources.Utility.CreateAuditTrail(dbEmployee, employee, Models.Enums.AuditTrailAction.Update, childListNames, 0);
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetEmployeeBO().Save(employee);
                if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    return transferObject;

                if (isNewEmployee)
                {
                    var emailTO = BizObjectFactory.GetEmailOutBoxBO().Save(emailOutBox);
                    if (emailTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    {
                        transferObject.StatusInfo = emailTO.StatusInfo;
                        return transferObject;
                    }
                }

                var auditTO = BizObjectFactory.GetAuditTrailBO().Save(auditTrail);
                if (auditTO.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                {
                    transferObject.StatusInfo = auditTO.StatusInfo;
                    return transferObject;
                }

                scope.Complete();
            }

            return transferObject;
        }

        public TransferObject<Employee> SaveForAttempt(Employee employee)
        {
            var transferObject = new TransferObject<Employee>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetEmployeeBO().Save(employee);
                if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    return transferObject;

                scope.Complete();
            }

            return transferObject;
        }

        public EmailOutBox GenerateEmailItem(Employee employee, FunctionURLs FunctionURLs)
        {
            EmailOutBox email = new EmailOutBox();
            email.IsBodyHtml = true;
            email.State = Models.Interfaces.State.Added;
            email.Sender = Models.Enums.EmailSender.General;
            email.To = employee.Email;
            email.Subject = "Welcome to VAMK FWMS";
            email.EmailStatus = EmailStatus.Pending;
            email.EmailSendAttempt = 0;

            EmployeeMessageModel messageModel = new EmployeeMessageModel();
            messageModel.FirstName = employee.FirstName;
            messageModel.UserName = employee.UserName;
            messageModel.Password = employee.Password;
            messageModel.FunctionURL = FunctionURLs.Login;

            email.MailContent = new EmailItemsFacade(websettings).GenerateEmailcontentFromTemplate<EmployeeMessageModel>(EmailTemplate.USER_REGISTRATION, messageModel);

            return email;
        }

        public TransferObject<Employee> SaveLockedEmployees(Employee employee)
        {
            var transferObject = new TransferObject<Employee>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetEmployeeBO().Save(employee);
                scope.Complete();
            }
            return transferObject;
        }
    }
}
