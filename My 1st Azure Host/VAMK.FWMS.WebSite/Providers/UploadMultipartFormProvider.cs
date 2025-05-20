using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web;

namespace VAMK.FWMS.WebSite.Providers
{
    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public UploadMultipartFormProvider(string rootPath) : base(rootPath) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null &&
                headers.ContentDisposition != null)
            {
                return String.Format("{0}~{1}",
                    GenerateUniqueRandomToken(),
                    headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"'));
            }

            Console.WriteLine(headers);

            return base.GetLocalFileName(headers);
        }

        public static string GenerateUniqueRandomToken()
        // generates a unique, random, and alphanumeric token
        {
            const string availableChars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            using (var generator = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                generator.GetBytes(bytes);
                var chars = bytes
                    .Select(b => availableChars[b % availableChars.Length]);
                var token = new string(chars.ToArray());
                return token;
            }
        }
    }
}