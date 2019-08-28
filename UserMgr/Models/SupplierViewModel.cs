using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class SupplierViewModel
    {
        [Required]
        [Display(Name = "供应商编码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$",ErrorMessage = "编码只能由4-40数字和字母构成")]
        public string SupplierCode { get; set; }

        [Required]
        [Display(Name = "供应商名称")]
        [StringLength(40,ErrorMessage = "长度在1-40",MinimumLength = 1)]
        public string SupplierName { get; set; }

        [Phone]
        [Display(Name = "联系方式")]
        public string SupplierPhoNum { get; set; }

        [EmailAddress]
        [Display(Name = "电子邮箱")]
        public string SupplierEmail { get; set; }

        [MaxLength(90,ErrorMessage = "太长了")]
        [Display(Name = "备注信息")]
        public string SupplierRemark { get; set; }
    }
}