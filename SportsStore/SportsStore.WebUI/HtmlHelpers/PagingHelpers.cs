using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using System.Text;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,PagingInfo PagingInfo,Func<int,string> PageUrl) 
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= PagingInfo.TotalPages;i++ )
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", PageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == PagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());

            }
            return MvcHtmlString.Create(result.ToString());
        }

    }
}