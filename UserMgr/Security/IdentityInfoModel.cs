﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserMgr.Security
{
    /// <summary>
    /// 用户身份验证信息模型
    /// </summary>
    public class IdentityInfoModel
    {
        /// <summary>
        /// 当前用户的ID
        /// </summary>
        public int CurUserID { get; set; }

        /// <summary>
        /// 当前用户的用户组ID
        /// </summary>
        public int CurUserGroupID { get; set; }

        /// <summary>
        /// 当前用户所在用户组的权限值
        /// </summary>
        public int CurUserGroupClass { get; set; }
    }
}