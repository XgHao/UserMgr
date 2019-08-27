using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class PageViewModel
    {
        [Required]
        public int PageID { get; set; }

        [Required]
        public string PageUrl{ get; set; }

        [Required]
        [Display(Name = "页面名称")]
        public string PageName { get; set; }

        [Required]
        [Display(Name = "访问权限")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "权限范围0-999")]
        public int PageClass { get; set; }

        public string BlackList { get; set; }

        public string WhiteList { get; set; }

        public IEnumerable<SelectListItem> UsersList { get; set; }
    }
}