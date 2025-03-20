using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.Common.Enums;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Enums;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.Models.Util;
using VAMK.FWMS.WebSite.Helpers;
using VAMK.FWMS.WebSite.Integrations.Helpers.Filters;
using VAMK.FWMS.WebSite.Integrations.Mobile.Filters;
using VAMK.FWMS.WebSite.Integrations.Mobile.Helpers;
using VAMK.FWMS.WebSite.Integrations.Mobile.ListModels;
using VAMK.FWMS.WebSite.Integrations.Mobile.Model;
using VAMK.FWMS.WebSite.Integrations.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Helpers
{
    public class EnumUtility
    {
        public static string GetDisplayTextForActivityType(VAMK.FWMS.Models.Enums.ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.MeetingWithCustomer:
                    return "Meeting With Customer";
                    break;
                case ActivityType.MeetingIternal:
                    return "Meeting Iternal";
                    break;
                case ActivityType.MeetingMMT:
                    return "Meeting MMT";
                    break;
            }
            return string.Empty;
        }
    }
}