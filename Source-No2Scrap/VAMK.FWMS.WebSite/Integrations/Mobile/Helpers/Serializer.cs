using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace VAMK.FWMS.WebSite.Integrations.Mobile.Helpers
{
    public static class Serializer
    {
        public static async Task<String> GetPostData(ApiController controller)
        {
            using (var contentStream = await controller.Request.Content.ReadAsStreamAsync())
            {
                contentStream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(contentStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static string GetObjectJson(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}