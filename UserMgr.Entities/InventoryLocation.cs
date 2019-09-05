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
    [SugarTable("InventoryLocation")]
    public partial class InventoryLocation
    {
        public InventoryLocation()
        {
            Enable = false;
        }
        /// <summary>
        /// Desc:库位ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int InventoryLocationID { get; set; }

        /// <summary>
        /// Desc:库区ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$",ErrorMessage = "请选择仓库")]
        [Display(Name = "库区")]
        public int? InventoryAreaID { get; set; }

        /// <summary>
        /// Desc:库位类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位类型")]
        public string InventoryLocationType { get; set; }

        /// <summary>
        /// Desc:库位编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位编号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "编号 只能由4-40个数字或字母组成")]
        public string InventoryLocationNo { get; set; }

        /// <summary>
        /// Desc:库位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位名称")]
        public string InventoryLocationName { get; set; }

        /// <summary>
        /// Desc:库位长度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位长度")]
        public decimal? InventoryLocationLength { get; set; }

        /// <summary>
        /// Desc:库位高度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位高度")]
        public decimal? InventoryLocationHeight { get; set; }

        /// <summary>
        /// Desc:库位宽度
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位宽度")]
        public decimal? InventoryLocationWidth { get; set; }

        /// <summary>
        /// Desc:排
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "排")]
        public int? Row { get; set; }

        /// <summary>
        /// Desc:层
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "层")]
        public int? Layer { get; set; }

        /// <summary>
        /// Desc:列
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "列")]
        public int? Line { get; set; }

        /// <summary>
        /// Desc:库位分组
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库位分组")]
        public int? InventoryLocationGroup { get; set; }

        /// <summary>
        /// Desc:库区巷道
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库区巷道")]
        public int? InventoryLocationNarrow { get; set; }

        /// <summary>
        /// Desc:入口距离
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "入口距离")]
        public int? EnterDistance { get; set; }

        /// <summary>
        /// Desc:出口距离
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "出口距离")]
        public int? ExitDistance { get; set; }

        /// <summary>
        /// Desc:前后
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "前后")]
        public int? FrontAndBack { get; set; }

        /// <summary>
        /// Desc:容器
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "容器")]
        public int? Container { get; set; }

        /// <summary>
        /// Desc:是否开启
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        public bool Enable { get; set; }

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
