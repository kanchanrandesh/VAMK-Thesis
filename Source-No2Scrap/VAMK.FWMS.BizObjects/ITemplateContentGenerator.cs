using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects
{
    public interface ITemplateContentGenerator
    {
        IEmailTemplateReader TemplateReader { get; set; }
        Dictionary<string, string> PlaceHolders { get; set; }
        string GenerateContent();
    }
}
