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
    [SugarTable("MaterialType")]
    public partial class MaterialType
    {
        public MaterialType()
        {


        }
        /// <summary>
        /// Desc:物资种类ID
        /// Default:
        /// Nullable:False
        /// </summary>            
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MaterialTypeID { get; set; }

        /// <summary>
        /// Desc:物资种类编码
        /// Default:
        /// Nullable:True
        /// </summary>             
        [Required]
        [Display(Name = "物资种类编码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "编码只能由4-40个数字或字母组成")]
        public string MaterialTypeNo { get; set; }

        /// <summary>
        /// Desc:物资种类名称
        /// Default:
        /// Nullable:True
        /// </summary>        
        [Required]
        [Display(Name = "物资种类名称")]
        [StringLength(40, ErrorMessage = "名称长度应该在1-40个字符", MinimumLength = 1)]
        public string MaterialTypeName { get; set; }

        /// <summary>
        /// Desc:父类ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MaterialTypeRoot { get; set; }

        /// <summary>
        /// Desc:供应商ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? SupplierID { get; set; }

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

        /// <summary>
        /// Desc:价格
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        //[Range(typeof(decimal), "0.00", "999999.99", ErrorMessage = "范围错误")]
        [Display(Name = "价格")]
        public decimal? MaterialTypePrice { get; set; }

        /// <summary>
        /// Desc:是否抛弃该条记录
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAbandon { get; set; }
    }
}
