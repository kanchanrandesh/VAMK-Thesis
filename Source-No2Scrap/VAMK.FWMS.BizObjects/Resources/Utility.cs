using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Interfaces;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VAMK.FWMS.BizObjects.Resources
{
    public class Utility
    {
        private SystemConfiguration systemConfiguration;

        internal Utility(SystemConfiguration _systemConfiguration)
        {
            systemConfiguration = _systemConfiguration;
        }

        /// <summary>
        /// Convert server datetime value to the domain's datetime value based on the domain's time zone.
        /// </summary>
        /// <param name="dataTime">DateTime value to convert</param>
        /// <returns></returns>
        internal DateTime ConvertToDomainTime(DateTime dateTime)
        {
            SystemConfiguration GFHelper = systemConfiguration;
            DateTime convertedVal = DateTime.MaxValue;
            TimeZoneInfo domainTimeZone = null;
            if (GFHelper.TimeZone != null)
            {
                try
                {
                    domainTimeZone = TimeZoneInfo.FindSystemTimeZoneById(GFHelper.TimeZone.Key);
                }
                catch
                {
                    domainTimeZone = GetDefaultTimeZone();
                }
                finally
                {
                    convertedVal = TimeZoneInfo.ConvertTime(dateTime, domainTimeZone);
                }
            }
            else
            {
                domainTimeZone = GetDefaultTimeZone();
                convertedVal = TimeZoneInfo.ConvertTime(dateTime, domainTimeZone);
            }
            return convertedVal;
        }

        private static TimeZoneInfo GetDefaultTimeZone()
        {
            return TimeZoneInfo.Local;
        }

        public static AuditTrail CreateAuditTrail(object dbObject, object newObject, Models.Enums.AuditTrailAction action, List<string> listNames, int employeeID)
        {
            var returnVal = new AuditTrail();
            returnVal.DetailList = new List<AuditTrailDetail>();
            returnVal.EntityType = newObject.GetType().ToString().Split(new[] { "VAMK.FWMS.Models." }, StringSplitOptions.None)[1];
            returnVal.EmployeeID = employeeID;
            returnVal.Date = DateTime.Now;
            returnVal.Action = action;
            returnVal.State = State.Added;

            if (action == Models.Enums.AuditTrailAction.Insert)
            {
                var detail = new AuditTrailDetail();
                detail.EntityType = returnVal.EntityType;
                detail.NewValue = newObject.GetType().GetProperty("AuditReference").GetValue(newObject, null).ToString();
                detail.Action = action;
                detail.State = State.Added;
                returnVal.DetailList.Add(detail);
            }
            else if (action == Models.Enums.AuditTrailAction.Delete)
            {
                returnVal.EntityID = (int)newObject.GetType().GetProperty("ID").GetValue(newObject, null);
                var detail = new AuditTrailDetail();
                detail.EntityType = returnVal.EntityType;
                detail.EntityID = returnVal.EntityID;
                detail.NewValue = newObject.GetType().GetProperty("AuditReference").GetValue(newObject, null).ToString();
                detail.Action = action;
                detail.State = State.Added;
                returnVal.DetailList.Add(detail);
            }
            else if (action == Models.Enums.AuditTrailAction.Update)
            {
                returnVal.EntityID = (int)newObject.GetType().GetProperty("ID").GetValue(newObject, null);

                foreach (var diff in Compare(dbObject, newObject).Differences)
                {
                    var detail = new AuditTrailDetail();
                    detail.EntityType = returnVal.EntityType;
                    detail.EntityID = returnVal.EntityID;
                    detail.Property = diff.PropertyName.Substring(1, diff.PropertyName.Length - 1);
                    detail.PreviousValue = diff.Object1Value != "(null)" ? diff.Object1Value : string.Empty;
                    detail.NewValue = diff.Object2Value;
                    detail.Action = action;
                    detail.State = State.Added;
                    returnVal.DetailList.Add(detail);
                }

                foreach (var child in listNames)
                {
                    var dbChildList = (IList)dbObject.GetType().GetProperty(child).GetValue(dbObject, null);
                    var newChildList = (IList)newObject.GetType().GetProperty(child).GetValue(newObject, null);

                    foreach (var newChild in newChildList)
                    {
                        int? id = (int?)newChild.GetType().GetProperty("ID").GetValue(newChild, null);

                        if (id == null)          
                        {
                            var detail = new AuditTrailDetail();
                            detail.EntityType = newChild.GetType().ToString().Split(new[] { "VAMK.FWMS.Models." }, StringSplitOptions.None)[1];
                            detail.NewValue = newChild.GetType().GetProperty("AuditReference").GetValue(newChild, null).ToString();
                            detail.Action = Models.Enums.AuditTrailAction.Insert;
                            detail.State = State.Added;
                            returnVal.DetailList.Add(detail);
                        }
                        else
                        {
                            foreach (var dbChild in dbChildList)
                            {
                                int dbChildId = (int)dbChild.GetType().GetProperty("ID").GetValue(dbChild, null);
                                if (id.Value == dbChildId)
                                {
                                    foreach (var diff in Compare(dbChild, newChild).Differences)
                                    {
                                        var detail = new AuditTrailDetail();
                                        detail.EntityType = newChild.GetType().ToString().Split(new[] { "VAMK.FWMS.Models." }, StringSplitOptions.None)[1];
                                        detail.EntityID = id;
                                        detail.Property = diff.PropertyName.Substring(1, diff.PropertyName.Length - 1);
                                        detail.PreviousValue = diff.Object1Value != "(null)" ? diff.Object1Value : string.Empty;
                                        detail.NewValue = diff.Object2Value;
                                        detail.Action = Models.Enums.AuditTrailAction.Update;
                                        detail.State = State.Added;
                                        returnVal.DetailList.Add(detail);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    //manage delete list
                    foreach (var dbChild in dbChildList)
                    {
                        int? dbChildId = (int?)dbChild.GetType().GetProperty("ID").GetValue(dbChild, null);
                        bool hasFound = false;
                        foreach (var newChild in newChildList)
                        {
                            int? newChildId = (int?)newChild.GetType().GetProperty("ID").GetValue(newChild, null);
                            if (dbChildId == newChildId)
                            {
                                hasFound = true;
                                break;
                            }
                        }
                        if (!hasFound)
                        {
                            var detail = new AuditTrailDetail();
                            detail.EntityType = dbChild.GetType().ToString().Split(new[] { "VAMK.FWMS.Models." }, StringSplitOptions.None)[1];
                            detail.EntityID = dbChildId;
                            detail.NewValue = dbChild.GetType().GetProperty("AuditReference").GetValue(dbChild, null).ToString();
                            detail.Action = Models.Enums.AuditTrailAction.Delete;
                            detail.State = State.Added;
                            returnVal.DetailList.Add(detail);
                        }
                    }
                }
            }

            return returnVal;
        }

        public static ComparisonResult Compare(object dbObject, object newObject)
        {
            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config.MaxDifferences = 100;
            compareLogic.Config.CompareChildren = false;

            return compareLogic.Compare(dbObject, newObject);
        }
    }
}
