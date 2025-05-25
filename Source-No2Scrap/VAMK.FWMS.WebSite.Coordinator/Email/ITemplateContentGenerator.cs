using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VAMK.FWMS.WebSite.Email
{
    public interface ITemplateContentGenerator<T>
    {
        string GenerateContent(StringBuilder template,T data);
    }
}