using VAMK.FWMS.BizObjects;
using VAMK.FWMS.BizObjects.Facades;
using VAMK.FWMS.Models;
using VAMK.FWMS.Models.Interfaces;
using VAMK.FWMS.Models.SearchQueries;
using VAMK.FWMS.WebSite.Filters;
using VAMK.FWMS.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using VAMK.FWMS.WebSite.Helpers;
using System.Linq;
using System.Net.Http;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Text;
using System.Net.Http.Headers;
using System.Net;

namespace VAMK.FWMS.WebSite.Controllers.WebAPI
{
    [RoutePrefix("api/stockCount")]

    public class StockCountController : ApiController
    {
        [HttpPost]
        [Route("printStockCountReport")]
        [HttpAuthorizeAccessRule(Rule = "STKCNTREPO")]
        public HttpResponseMessage PrintPettyCashReport(VAMK.FWMS.WebSite.ReportModels.StockCountReportModel model)
        {
            VAMK.FWMS.Models.ReportFilters.StockCountReportFilter stockCountReportFilter = model;
            Document document = new Document(PageSize.A4.Rotate(), 25, 25, 10, 25);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document = new StockCountFacade(WebSettingProvider.GetWebSettings()).GenerateStockCountReport(stockCountReportFilter, document);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                var fileName = new StringBuilder();
                fileName.Append("StockCountReport_");
                fileName.Append(DateTime.Now.Month.ToString().PadLeft(2, '0'));
                fileName.Append(DateTime.Now.Day.ToString().PadLeft(2, '0'));
                fileName.Append(DateTime.Now.Hour.ToString().PadLeft(2, '0'));
                fileName.Append(DateTime.Now.Minute.ToString().PadLeft(2, '0'));
                fileName.Append(".pdf");

                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new ByteArrayContent(bytes.ToArray());
                httpResponseMessage.Content.Headers.Add("x-filename", fileName.ToString());
                httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
                return httpResponseMessage;
            }
        }
    }
}