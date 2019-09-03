using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PickingTask")]
    public partial class PickingTask
    {
           public PickingTask(){


           }
           /// <summary>
           /// Desc:拣货任务单ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long PickingTaskID {get;set;}

           /// <summary>
           /// Desc:拣货单编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PickingTaskNo {get;set;}

           /// <summary>
           /// Desc:拣货单类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PickingTaskType {get;set;}

           /// <summary>
           /// Desc:波次编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WavePickingNo {get;set;}

           /// <summary>
           /// Desc:拣货工位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PickingStation {get;set;}

           /// <summary>
           /// Desc:优先级
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Priority {get;set;}

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
           /// Desc:录入人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Creater {get;set;}

           /// <summary>
           /// Desc:录入时间
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
