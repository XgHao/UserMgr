using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PickingTaskDetail")]
    public partial class PickingTaskDetail
    {
           public PickingTaskDetail(){


           }
           /// <summary>
           /// Desc:拣货单明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long PickingTaskDetailID {get;set;}

           /// <summary>
           /// Desc:拣货任务单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? PickingTaskID {get;set;}

           /// <summary>
           /// Desc:波次明细单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? WavePickingDetailID {get;set;}

           /// <summary>
           /// Desc:物资规格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SizeCode {get;set;}

           /// <summary>
           /// Desc:托盘条码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrayCode {get;set;}

           /// <summary>
           /// Desc:托盘明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? TrayDetailID {get;set;}

           /// <summary>
           /// Desc:分配数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QuantityAllotted {get;set;}

           /// <summary>
           /// Desc:实际拣货数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ActualPickingNum {get;set;}

           /// <summary>
           /// Desc:托盘总数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TrayCount {get;set;}

           /// <summary>
           /// Desc:容器
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Container {get;set;}

           /// <summary>
           /// Desc:拣货时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? PickingTime {get;set;}

           /// <summary>
           /// Desc:拣货人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Picker {get;set;}

           /// <summary>
           /// Desc:拣货工位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PickerStation {get;set;}

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
