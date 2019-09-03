using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CheckTask")]
    public partial class CheckTask
    {
           public CheckTask(){


           }
           /// <summary>
           /// Desc:盘点任务单ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int CheckTaskID {get;set;}

           /// <summary>
           /// Desc:关联盘点任务单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RelCheckTaskID {get;set;}

           /// <summary>
           /// Desc:盘点单类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CheckTaskType {get;set;}

           /// <summary>
           /// Desc:盘点单编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CheckTaskNo {get;set;}

           /// <summary>
           /// Desc:盘点日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CheckTaskDate {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Creater {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Changer {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ChangeTime {get;set;}

           /// <summary>
           /// Desc:数据版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataVersion {get;set;}

    }
}
