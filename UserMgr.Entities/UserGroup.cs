using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("UserGroup")]
    public partial class UserGroup
    {
           public UserGroup(){


           }
           /// <summary>
           /// Desc:用户组ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int UserGroupID {get;set;}

           /// <summary>
           /// Desc:用户组名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserGroupName {get;set;}

           /// <summary>
           /// Desc:用户组编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserGroupCode {get;set;}

           /// <summary>
           /// Desc:用户组等级（用于识别访问权限）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserGroupClass {get;set;}

           /// <summary>
           /// Desc:菜单组编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MenuGroupCode {get;set;}

           /// <summary>
           /// Desc:用户组描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserGroupDesc {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserGroupCreater {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UserGroupCreateTime {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserGroupChanger {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UserGroupChangeTime {get;set;}

    }
}
