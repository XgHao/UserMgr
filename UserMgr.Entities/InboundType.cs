using System;
using System.Linq;
using System.Text;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

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
            IsAbandon = false;
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
        [Required]
        [Display(Name = "入库类型名称")]
        [MaxLength(20)]
        public string InboundTypeName { get; set; }

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
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

    }
}
