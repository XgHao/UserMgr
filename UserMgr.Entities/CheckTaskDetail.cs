using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CheckTaskDetail")]
    public partial class CheckTaskDetail
    {
           public CheckTaskDetail(){


           }
           /// <summary>
           /// Desc:盘点任务单明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int CheckTaskDetailID {get;set;}

           /// <summary>
           /// Desc:盘点任务单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CheckTaskID {get;set;}

           /// <summary>
           /// Desc:库位ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryLocationID {get;set;}

           /// <summary>
           /// Desc:物资规格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MaterialSizeID {get;set;}

           /// <summary>
           /// Desc:物资数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MaterialNum {get;set;}

           /// <summary>
           /// Desc:托盘明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? TrayDetailID {get;set;}

           /// <summary>
           /// Desc:拣货站台(工位)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PickingStation {get;set;}

           /// <summary>
           /// Desc:实际数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ActualNum {get;set;}

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
