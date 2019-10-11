﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WavePickingType")]
    public partial class WavePickingType
    {
        public WavePickingType()
        {
            IsAbandon = false;
        }
        /// <summary>
        /// Desc:波次类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int WavePickingTypeID { get; set; }

        /// <summary>
        /// Desc:波次类型名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WavePickingTypeName { get; set; }

        /// <summary>
        /// Desc:抛弃标识
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAbandon { get; set; }

        /// <summary>
        /// Desc:预留字段
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Other { get; set; }

    }
}
