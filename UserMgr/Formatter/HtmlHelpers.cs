using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserMgr.Entities.View;

namespace UserMgr.Formatter
{
    /// <summary>
    /// Html辅助器
    /// </summary>
    public static class HtmlHelpers
    {
        public static MvcHtmlString Span(this HtmlHelper html, View_InboundTask view_InboundTask)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("span");
            tag.InnerHtml = view_InboundTask.StatusName;
            tag.AddCssClass($"label-{view_InboundTask.HtmlAttributes}");
            tag.AddCssClass("label");
            result.Append(tag.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}