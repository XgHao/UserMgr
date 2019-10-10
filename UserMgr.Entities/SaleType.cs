using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SaleType")]
    public partial class SaleType
    {
        public SaleType()
        {


        }
        /// <summary>
        /// Desc:销售类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SaleTypeID { get; set; }

        /// <summary>
        /// Desc:销售类型名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SaleTypeName { get; set; }

        /// <summary>
        /// Desc:预留字段
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Other { get; set; }

    }
}
