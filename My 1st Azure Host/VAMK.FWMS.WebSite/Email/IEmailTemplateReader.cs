using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.WebSite.Email
{
    public interface IEmailTemplateReader
    {
        string filePath { get; set; }
        Task<StringBuilder> ReadAllLinesAsync(string path, Encoding encoding);


    }
}
