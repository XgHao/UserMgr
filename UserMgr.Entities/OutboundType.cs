using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("OutboundType")]
    public partial class OutboundType
    {
        public OutboundType()
        {
            IsAbandon = false;
        }
        /// <summary>
        /// Desc:出库类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int OutboundTypeID { get; set; }

        /// <summary>
        /// Desc:出库类型名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OutboundTypeName { get; set; }

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
