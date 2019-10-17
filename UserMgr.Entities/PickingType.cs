﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PickingType")]
    public partial class PickingType
    {
        public PickingType()
        {
            IsAbandon = false;
        }
        /// <summary>
        /// Desc:拣货类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PickingTypeID { get; set; }

        /// <summary>
        /// Desc:拣货类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PickingTypeName { get; set; }

        /// <summary>
        /// Desc:抛弃标识
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAbandon { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Creater { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:修改人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Changer { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }

        /// <summary>
        /// Desc:数据版本
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

        /// <summary>
        /// Desc:预留字段
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Other { get; set; }

    }
}
