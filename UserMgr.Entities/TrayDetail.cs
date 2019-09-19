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
    [SugarTable("TrayDetail")]
    public partial class TrayDetail
    {
        public TrayDetail()
        {


        }
        /// <summary>
        /// Desc:托盘明细ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long TrayDetailID { get; set; }

        /// <summary>
        /// Desc:入库单明细ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择关联的入库任务明细单")]
        public int? InboundTaskDetailID { get; set; }

        /// <summary>
        /// Desc:托盘ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择关联的托盘")]
        public long? TrayID { get; set; }

        /// <summary>
        /// Desc:组盘顺序
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "组盘顺序")]
        public int? GroupTrayOrder { get; set; }

        /// <summary>
        /// Desc:物资ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择关联的物资规格")]
        public int? MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:小件数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "小件数量")]
        public decimal? ParcelMeasure { get; set; }

        /// <summary>
        /// Desc:物料SN
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料SN")]
        public string MaterialSN { get; set; }

        /// <summary>
        /// Desc:入库过账标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "入库过账标识")]
        public int? InboundPostMark { get; set; }

        /// <summary>
        /// Desc:出库过账标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "出库过账标识")]
        public int? OutboundPostMark { get; set; }

        /// <summary>
        /// Desc:生产日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "生产日期")]
        public DateTime? ProductionDate { get; set; }

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
