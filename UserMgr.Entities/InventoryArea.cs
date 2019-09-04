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
    [SugarTable("InventoryArea")]
    public partial class InventoryArea
    {
        public InventoryArea()
        {
            Enable = false;
        }
        /// <summary>
        /// Desc:库位ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int InventoryAreaID { get; set; }

        /// <summary>
        /// Desc:仓库ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression("^(0|[1-9][0-9]*)$",ErrorMessage = "请选择仓库")]
        public int? WarehouseID { get; set; }

        /// <summary>
        /// Desc:库区编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库区编号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "规格代码只能由4-40个数字或字母组成")]
        public string InventoryAreaNo { get; set; }

        /// <summary>
        /// Desc:库区名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库区名称")]
        public string InventoryAreaName { get; set; }

        /// <summary>
        /// Desc:是否开放
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool Enable { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "备注")]
        [StringLength(100,MinimumLength = 0)]
        public string Remark { get; set; }

        /// <summary>
        /// Desc:库区类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "库区类型")]
        public string InventoryAreaType { get; set; }

        /// <summary>
        /// Desc:其他信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "其他信息")]
        [StringLength(100,MinimumLength = 0)]
        public string OtherInfo { get; set; }

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
