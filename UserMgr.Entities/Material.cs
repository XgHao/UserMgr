﻿using System;
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
        /// Desc:物资规格ID
        /// Default:
        /// Nullable:False
        /// </summary>         
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:物资种类ID
        /// Default:
        /// Nullable:True
        /// </summary>         
        [Required(ErrorMessage = "请选择一项物料种类")]
        [Display(Name = "物料种类选项")]
        [RegularExpression("^(0|[1-9][0-9]*)$", ErrorMessage = "请选择物资种类")]
        public int? MaterialTypeID { get; set; }

        /// <summary>
        /// Desc:规格代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "规格代码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "规格代码只能由4-40个数字或字母组成")]
        public string SizeCode { get; set; }

        /// <summary>
        /// Desc:详细描述
        /// Default:
        /// Nullable:True
        /// </summary>      
        [Required]
        [Display(Name = "详细描述")]
        [StringLength(50, ErrorMessage = "名称长度应该在1-50个字符", MinimumLength = 1)]
        public string Detail { get; set; }

        /// <summary>
        /// Desc:单位
        /// Default:
        /// Nullable:True
        /// </summary>          
        //[Required]
        //[Display(Name = "单位")]
        //[MaxLength(20,ErrorMessage = "单位太长了")]
        public string Unit { get; set; }

        /// <summary>
        /// Desc:长
        /// Default:
        /// Nullable:True
        /// </summary>         
        [Required]
        [Display(Name = "长")]
        public decimal? Length { get; set; }

        /// <summary>
        /// Desc:宽
        /// Default:
        /// Nullable:True
        /// </summary>        
        [Required]
        [Display(Name = "宽")]
        public decimal? Width { get; set; }

        /// <summary>
        /// Desc:高
        /// Default:
        /// Nullable:True
        /// </summary>       
        [Required]
        [Display(Name = "高")]
        public decimal? Height { get; set; }

        /// <summary>
        /// Desc:重量
        /// Default:
        /// Nullable:True
        /// </summary>      
        [Required]
        [Display(Name = "质量")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// Desc:物资型号
        /// Default:
        /// Nullable:True
        /// </summary>     
        [Required]
        [Display(Name = "物资型号")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "型号只能由4-40个数字或字母组成")]
        public string MaterialModel { get; set; }

        /// <summary>
        /// Desc:小件单位
        /// Default:
        /// Nullable:True
        /// </summary>        
        //[Required]
        //[Display(Name = "小件单位")]
        public string ParcelUnit { get; set; }

        /// <summary>
        /// Desc:小件计量
        /// Default:
        /// Nullable:True
        /// </summary>          
        [Display(Name = "小件计量")]
        public int? ParcelMeasure { get; set; }

        /// <summary>
        /// Desc:物料最低值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料最低值")]
        public int? MaterialMin { get; set; }

        /// <summary>
        /// Desc:物料最高值
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "物料最高值")]
        public int? MaterialMax { get; set; }

        /// <summary>
        /// Desc:物料容器
        /// Default:
        /// Nullable:True
        /// </summary>        
        [Display(Name = "物料容器")]
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择容器")]
        public int? MaterialContainer { get; set; }

        /// <summary>
        /// Desc:是否组合商务
        /// Default:
        /// Nullable:True
        /// </summary>          
        //[Required]
        //[Display(Name = "是否组合商品")]
        public bool? IsCKD { get; set; }

        /// <summary>
        /// Desc:物料密度
        /// Default:
        /// Nullable:True
        /// </summary>        
        [Required]
        [Display(Name = "物料密度")]
        public decimal? MaterialDensity { get; set; }

        /// <summary>
        /// Desc:数据版本
        /// Default:
        /// Nullable:True
        /// </summary>         
        public int? DataVersion { get; set; }

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
        /// Desc:是否抛弃该条记录
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAbandon { get; set; }
    }
}
