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
    [SugarTable("Material")]
    public partial class Material
    {
        public Material()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required(ErrorMessage = "请选择一项物料种类")]
        [Display(Name = "物料种类选项")]
        public int? MaterialTypeID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "规格代码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "规格代码只能由4-40个数字或字母组成")]
        public string SizeCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "详细描述")]
        [StringLength(50, ErrorMessage = "名称长度应该在1-50个字符", MinimumLength = 1)]
        public string Detail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        //[Required]
        //[Display(Name = "单位")]
        //[MaxLength(20,ErrorMessage = "单位太长了")]
        public string Unit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "长")]
        public decimal? Length { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "宽")]
        public decimal? Width { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "高")]
        public decimal? Height { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "质量")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物资型号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "型号只能由4-40个数字或字母组成")]
        public string MaterialModel { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        //[Required]
        //[Display(Name = "小件单位")]
        public string ParcelUnit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "小件计量")]
        public int? ParcelMeasure { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料最低值")]
        public int? MaterialMin { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料最高值")]
        public int? MaterialMax { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "物料容器")]
        public int? MaterialContainer { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        //[Required]
        //[Display(Name = "是否组合商品")]
        public bool? IsCKD { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料密度")]
        public decimal? MaterialDensity { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Creater { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Changer { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }

    }
}
