using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InboundType")]
    public partial class InboundType
    {
        public InboundType()
        {


        }
        /// <summary>
        /// Desc:入库类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int InboundTypeID { get; set; }

        /// <summary>
        /// Desc:入库类型名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string InboundTypeName { get; set; }

        /// <summary>
        /// Desc:预留字段
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Other { get; set; }

    }
}
