using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Menu")]
    public partial class Menu
    {
        public Menu()
        {


        }
        /// <summary>
        /// Desc:菜单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MenuID { get; set; }

        /// <summary>
        /// Desc:菜单索引
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MenuIndex { get; set; }

        /// <summary>
        /// Desc:菜单名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuName { get; set; }

        /// <summary>
        /// Desc:菜单英文名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuEName { get; set; }

        /// <summary>
        /// Desc:菜单编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MenuNo { get; set; }

        /// <summary>
        /// Desc:菜单父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MenuRoot { get; set; }

        /// <summary>
        /// Desc:菜单窗口名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuWindowName { get; set; }

        /// <summary>
        /// Desc:菜单描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuDesc { get; set; }

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
