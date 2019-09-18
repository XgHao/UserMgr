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
    [SugarTable("OutboundTaskDetail")]
    public partial class OutboundTaskDetail
    {
        public OutboundTaskDetail()
        {


        }
        /// <summary>
        /// Desc:出库任务单明细ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int OutboundTaskDetailID { get; set; }

        /// <summary>
        /// Desc:出库任务单ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        public int? OutboundTaskID { get; set; }

        /// <summary>
        /// Desc:物资规格ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择物资")]
        public int? MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:物资数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物资数量")]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "范围在0-999999")]
        public decimal? MaterialNum { get; set; }

        /// <summary>
        /// Desc:件重
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "件重")]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "范围在0-999999")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Desc:批号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "批号")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Desc:送货单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "送货单位")]
        public string DeliveryUnit { get; set; }

        /// <summary>
        /// Desc:车号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "车号")]
        public string CarNum { get; set; }

        /// <summary>
        /// Desc:冰衣
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "冰衣")]
        public string Glaze { get; set; }

        /// <summary>
        /// Desc:单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "单位")]
        public string Unit { get; set; }

        /// <summary>
        /// Desc:分配数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "分配数量")]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "范围在0-999999")]
        public decimal? QuantityAllotted { get; set; }

        /// <summary>
        /// Desc:发货数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "发货数量")]
        [RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "范围在0-999999")]
        public decimal? DispatchNum { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

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
