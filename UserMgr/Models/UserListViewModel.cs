using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class UserListViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "范围0-999")]
        public int UserGroupID { get; set; }

        [Required]
        public string UserName { get; set; }

        public string UserDesc { get; set; }

        public int UserCreater { get; set; }

        public DateTime UserCreateTime { get; set; }


        public IEnumerable<SelectListItem> UserGroupList { get; set; }

    }
}