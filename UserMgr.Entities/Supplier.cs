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
    [SugarTable("Supplier")]
    public partial class Supplier
    {
        public Supplier()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int SupplierID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required]
        [Display(Name = "供应商编码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$",ErrorMessage = "编码只能由4-40个数字或字母组成")]
        public string SupplierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "供应商名称")]
        [StringLength(40,ErrorMessage = "名称长度应该在1-40个字符",MinimumLength = 1)]
        public string SupplierName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Phone]
        [Display(Name = "联系方式")]
        public string SupplierPhoNum { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [EmailAddress]
        [Display(Name = "电子邮箱")]
        public string SupplierEmail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [MaxLength(90,ErrorMessage = "备注太长了，请控制在90个字符以内")]
        [Display(Name = "备注信息")]
        public string SupplierRemark { get; set; }

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