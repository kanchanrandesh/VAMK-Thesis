using VAMK.FWMS.BizObjects;
using VAMK.FWMS.WebSite.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/timeZone")]
    public class TimeZoneController : ApiController
    {
        [HttpGet]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            var returnList = new List<TimeZoneModel>();
            foreach (var item in BizObjectFactory.GetTimeZoneBO().GetAll())
                returnList.Add((TimeZoneModel)item);

            return Ok(returnList);
        }
    }
}