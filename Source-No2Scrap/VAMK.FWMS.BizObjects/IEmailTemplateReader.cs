using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface IEmailTemplateReader
    {
        string filePath { get; set; }
        StringBuilder ReadAllLines(string path, Encoding encoding);
    }
}
