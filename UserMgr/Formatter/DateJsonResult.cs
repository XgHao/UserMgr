using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserMgr.Formatter
{
    /// <summary>
    /// Json时间格式化
    /// </summary>
    public class DateJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context==null)
            {
                throw new ArgumentException("context");
            }

            HttpResponseBase responseBase = context.HttpContext.Response;

            if (Data != null) 
            {
                IsoDateTimeConverter DateConverter = new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                };
                responseBase.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, DateConverter));
            }
        }
    }
}