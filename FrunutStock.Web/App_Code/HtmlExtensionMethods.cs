using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Web.App_Code
{
    public static class HtmlExtensionMethods
    {
        public static IHtmlString LabelWithMark(this HtmlHelper helper, string content)
        {
            string htmlString = String.Format("<label><mark>{0}</mark></label>", content);
            return new HtmlString(htmlString);
        }

    }
}