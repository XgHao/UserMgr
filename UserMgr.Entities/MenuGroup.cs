using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("MenuGroup")]
    public partial class MenuGroup
    {
        public MenuGroup()
        {


        }
        /// <summary>
        /// Desc:菜单组ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MenuGroupID { get; set; }

        /// <summary>
        /// Desc:菜单组编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuGroupNo { get; set; }

        /// <summary>
        /// Desc:菜单组名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuGroupName { get; set; }

        /// <summary>
        /// Desc:菜单ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MenuID { get; set; }

        /// <summary>
        /// Desc:菜单组描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuGroupDesc { get; set; }

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

    }
}
