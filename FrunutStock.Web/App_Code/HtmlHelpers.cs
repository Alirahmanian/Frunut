using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Web.App_Code
{
    public class HtmlHelpers
    {
       
        public static IHtmlString LabelWithMark(string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>", content);
            return new HtmlString(htmlString);
        }
       
    }
}