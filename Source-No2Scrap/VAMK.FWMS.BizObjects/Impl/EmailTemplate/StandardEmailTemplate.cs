using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects.Impl.EmailTemplate
{
    public class StandardEmailTemplate : ITemplateContentGenerator
    {
        Dictionary<string, string> placeHolders;
        IEmailTemplateReader templateReader;
        StringBuilder template;
        string placeHolder_prefix;
        string placeHolder_postfix;

        public StandardEmailTemplate(string teplatePath, IEmailTemplateReader templateReader)
        {
            placeHolders = new Dictionary<string, string>();
            this.templateReader = templateReader;
            template = templateReader.ReadAllLines(teplatePath, Encoding.UTF8);
            GetPlaceHoldersFromTemplate(template);
        }

        #region Properties
        public Dictionary<string, string> PlaceHolders
        {
            get
            {
                return placeHolders;
            }

            set
            {
                placeHolders = value;
            }
        }

        public string PlaceHolder_Postfix
        {
            get
            {
                return placeHolder_postfix;
            }

            set
            {
                placeHolder_postfix = value;
            }
        }

        public string PleaceHolder_Prefix
        {
            get
            {
                return placeHolder_prefix;
            }

            set
            {
                placeHolder_prefix = value;
            }
        } 
        #endregion

        public IEmailTemplateReader TemplateReader
        {
            get
            {
                return templateReader;
            }

            set
            {
                templateReader = value;
            }
        }

        public string GenerateContent()
        {
            try
            {

                foreach (var item in placeHolders)
                {
                    if (item.Value != null)
                    {
                        template.Replace(String.Format("{0}{1}{2}", "{{", item.Key, "}}"), item.Value); 
                    }
                    else
                    {
                        template.Replace(String.Format("{0}{1}{2}", "{{", item.Key, "}}"), String.Format("< !--{0} {1}-- >",item.Key," is null"));
                    }
                }

                return template.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Template Generation Exception", ex);
            }
        }

        private void GetPlaceHoldersFromTemplate(StringBuilder template)
        {
            //string pattern = @"(?<={{).*?(?=}})";
            //Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            //Match m = r.Match(template.ToString());
            //while (m.Success)
            //{
            //    if(!placeHolders.ContainsKey(m.Value))
            //    {
            //        placeHolders.Add(m.Value, String.Empty);
            //    }
            //}
        }
    }
}
