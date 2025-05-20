using VAMK.FWMS.Common;
using VAMK.FWMS.Common.Envelop;
using VAMK.FWMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Transactions;

namespace VAMK.FWMS.BizObjects.Facades
{
    public class ContactPersonFacade
    {
        public TransferObject<ContactPerson> Save(ContactPerson contactPerson)
        {
            AuditTrail auditTrail = null;
            if (contactPerson.ID == null)
                auditTrail = Resources.Utility.CreateAuditTrail(null, contactPerson, Models.Enums.AuditTrailAction.Insert, new List<string>(), 0);
            else
            {
                var dbContact = BizObjectFactory.GetContactPersonBO().GetSingle(contactPerson.ID.Value);

                auditTrail = Resources.Utility.CreateAuditTrail(dbContact, contactPerson, Models.Enums.AuditTrailAction.Update, new List<string>(), 0);
            }

            var transferObject = new TransferObject<ContactPerson>();
            var options = new TransactionOptions();
            options.Timeout = TimeSpan.FromMinutes(1);
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                transferObject = BizObjectFactory.GetContactPersonBO().Save(contactPerson);
                if (transferObject.StatusInfo.Status != Common.Enums.ServiceStatus.Success)
                    return transferObject;

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

        public OperationStatus ExportToExcel()
        {
            var operationStatus = new OperationStatus();
            var contactsPerson = BizObjectFactory.GetContactPersonBO().GetAllForExport();

            // Load Excel application
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // Create empty workbook
            excel.Workbooks.Add();

            // Create Worksheet from active sheet
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

            try
            {
                // ------------------------------------------------
                // Creation of header cells
                // ------------------------------------------------
                workSheet.Cells[1, "A"] = "Account Manager";
                workSheet.Cells[1, "B"] = "Organization";
                workSheet.Cells[1, "C"] = "Title";
                workSheet.Cells[1, "D"] = "First Name";
                workSheet.Cells[1, "E"] = "Last Name";
                workSheet.Cells[1, "F"] = "Job Role";
                workSheet.Cells[1, "G"] = "Designation Category";
                workSheet.Cells[1, "H"] = "Mobile";
                workSheet.Cells[1, "I"] = "Phone";
                workSheet.Cells[1, "J"] = "Email";

                // ------------------------------------------------
                // Populate sheet with some real data from "Contacts" list
                // ------------------------------------------------
                int row = 2; // start row (in row 1 are header cells)

                foreach (var contact in contactsPerson)
                {
                    workSheet.Cells[row, "C"] = contact.Code;
                    workSheet.Cells[row, "C"] = contact.Name;
                    workSheet.Cells[row, "H"] = contact.Mobile;
                    workSheet.Cells[row, "I"] = contact.PhoneNumber;
                    workSheet.Cells[row, "J"] = contact.Email;

                    row++;
                }

                // Apply some predefined styles for data to look nicely :)
                //workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatReport1);

                // Define filename
                string fileName = string.Format(@"{0}\Contacts{1}.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());

                // Save this data as a file
                workSheet.SaveAs(fileName);

                operationStatus.Message = string.Format("The file '{0}' is saved successfully!", fileName);
                operationStatus.Status = Common.Enums.ServiceStatus.Success.ToString();
            }
            catch (Exception exception)
            {
                operationStatus.Message = exception.Message;
                operationStatus.Message = Common.Enums.ServiceStatus.Error.ToString();
            }
            finally
            {
                // Quit Excel application
                excel.Quit();

                // Release COM objects (very important!)
                if (excel != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                if (workSheet != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                // Empty variables
                excel = null;
                workSheet = null;

                // Force garbage collector cleaning
                GC.Collect();
            }
            return operationStatus;
        }
    }
}