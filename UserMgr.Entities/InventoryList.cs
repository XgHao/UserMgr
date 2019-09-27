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
    [SugarTable("InventoryList")]
    public partial class InventoryList
    {
        public InventoryList()
        {


        }
        /// <summary>
        /// Desc:库存清单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long InventoryListID { get; set; }

        /// <summary>
        /// Desc:入库任务明细ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择入库任务细节单")]
        public int? InboundTaskDetailID { get; set; }

        /// <summary>
        /// Desc:出库任务明细ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择出库任务细节单")]
        public int? OutboundTaskDetailID { get; set; }

        /// <summary>
        /// Desc:库存类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库存类型")]
        public string InventoryType { get; set; }

        /// <summary>
        /// Desc:托盘ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择托盘")]
        public long? TrayID { get; set; }

        /// <summary>
        /// Desc:库位ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择库位")]
        public int? InventoryLocationID { get; set; }

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
