﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class InboundTypeViewModel : InboundType
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public InboundType InitAddInboundType(int creater) => Formatterr.InitAddModel<InboundType>(this, creater);
    }
}