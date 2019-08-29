﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class PageViewModel : Page
    {
        public IEnumerable<SelectListItem> UsersList { get; set; }
    }
}