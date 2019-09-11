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
    [SugarTable("InboundTask")]
    public partial class InboundTask
    {
        public InboundTask()
        {


        }
        /// <summary>
        /// Desc:入库任务单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int InboundTaskID { get; set; }

        /// <summary>
        /// Desc:入库任务单编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "入库任务单编码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "编码只能由4-40个数字或字母组成")]
        public string InboundTaskNo { get; set; }

        /// <summary>
        /// Desc:入库类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "入库类型")]
        public string InboundType { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string InboundRemark { get; set; }

        /// <summary>
        /// Desc:到货时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AOGTime { get; set; }

        /// <summary>
        /// Desc:供应商ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择供应商")]
        public int? SupplierID { get; set; }

        /// <summary>
        /// Desc:外部单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "外部单号")]
        public string ExterNo { get; set; }

        /// <summary>
        /// Desc:采购单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "采购单号")]
        public string OrderNo { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:任务完成时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TaskCompletionTime { get; set; }

        /// <summary>
        /// Desc:录入人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Creater { get; set; }

        /// <summary>
        /// Desc:录入时间
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
