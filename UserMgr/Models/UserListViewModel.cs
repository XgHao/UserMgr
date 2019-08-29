using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserMgr.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Models
{
    public class UserListViewModel : User
    {
        public IEnumerable<SelectListItem> UserGroupList { get; set; }

    }
}