using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Common.Utility;
using VAMK.FWMS.Models;
using System;
using System.Text;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class AccountFacade
    {
        public TransferObject<SystemUser> ChangePassword(int employeeId, string currentPassword, string newPassword, string conformPassword)
        {
            CryptoProvider crypto = new CryptoProvider();
            var transferObject = new TransferObject<SystemUser>();

            var employee = BizObjectFactory.GetEmployeeBO().GetSingle(employeeId);

            if (!string.IsNullOrEmpty(newPassword))
            {
                bool hasUpperCaseLetter = false;
                bool hasLowerCaseLetter = false;
                bool hasDecimalDigit = false;

                if (newPassword.Length < 6)
                {
                    transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                    transferObject.StatusInfo.Message = "Minimum number of characters for a password should be six";
                    return transferObject;
                }

                foreach (char c in newPassword)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
                if (!hasUpperCaseLetter)
                {
                    transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                    transferObject.StatusInfo.Message = "Password should contain at least one upper case letter";
                    return transferObject;
                }
                else if (!hasLowerCaseLetter)
                {
                    transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                    transferObject.StatusInfo.Message = "Password should contain at least one lower case letter";
                    return transferObject;
                }
                else if (!hasDecimalDigit)
                {
                    transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                    transferObject.StatusInfo.Message = "Password should contain at least one numeric value";
                    return transferObject;
                }
            }

            if (employee.Password != crypto.GetHash(currentPassword.Trim()))
            {
                transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                transferObject.StatusInfo.Message = "Current Password is invalid";
                return transferObject;
            }
            else if (crypto.GetHash(newPassword) != crypto.GetHash(conformPassword))
            {
                transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                transferObject.StatusInfo.Message = "Password does not match the confirm password";
                return transferObject;
            }
            else
            {
                employee.Password = crypto.GetHash(newPassword);
                employee.State = Models.Interfaces.State.Modified;
                transferObject = BizObjectFactory.GetEmployeeBO().Save(employee);
                transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.Success;
                transferObject.StatusInfo.Message = "Password Successfully Updated";

            }

            return transferObject;
        }

        public TransferObject<bool> ResetPassword(string userName)
        {
            CryptoProvider crypto = new CryptoProvider();
            var transferObject = new TransferObject<bool>();

            var employee = BizObjectFactory.GetEmployeeBO().Search(new Models.SearchQueries.EmployeeSearchQuery() { UserName = userName });
            if (employee != null && employee.Count > 0)
            {
                string newTempPassword = System.Web.Security.Membership.GeneratePassword(12, 3);
                employee[0].Password = crypto.GetHash(newTempPassword);
                employee[0].State = Models.Interfaces.State.Modified;
                BizObjectFactory.GetEmployeeBO().Save(employee[0]);

                StringBuilder str = new StringBuilder();
                str.Append("Dear ");
                str.Append(employee[0].FirstName);
                str.Append(" ");
                str.Append(employee[0].LastName);
                str.Append("\n");
                str.Append("\n");
                str.Append("You recently requested to reset your password of FWMS user account.");
                str.Append("\n");
                str.Append("\n");
                str.Append("Please find below password to access the system.");
                str.Append("\n");
                str.Append("\n");
                str.Append("Password  : ");
                str.Append(newTempPassword);
                str.Append("\n");
                str.Append("\n");
                str.Append("Thank You");
                str.Append("\n");
                str.Append("VAMK FWMS");

                new EmailItemsFacade(null).AddEmailToOutBox(new EmailOutBox()
                {
                    ID = null,
                    CreatedByID = 1,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    EmailCreatedDate = DateTime.Now,
                    EmailSendAttempt = 0,
                    EmailStatus = Models.Enums.EmailStatus.Pending,
                    To = employee[0].Email,
                    Subject = "FWMS - Password Reset",
                    MailContent = str.ToString(),
                    User = "System",
                    State = Models.Interfaces.State.Added

                });

                transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.Success;
                transferObject.StatusInfo.Message = "A new password has been sent to your email address";
            }
            else
            {
                transferObject.StatusInfo.Status = Common.Enums.ServiceStatus.DataValidationError;
                transferObject.StatusInfo.Message = "Invalid Username";

            }
            return transferObject;
        }
    }
}
