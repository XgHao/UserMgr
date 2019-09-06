using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities.View
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("View_User")]
    public partial class View_User
    {
        public View_User()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int UserID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserNo { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserGroupName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserPhoneNum { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserEmail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserDesc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UserCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsUse { get; set; }

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
        public string CreaterName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ChangerName { get; set; }

    }
}
