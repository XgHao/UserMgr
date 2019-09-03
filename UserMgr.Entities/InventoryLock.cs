using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InventoryLock")]
    public partial class InventoryLock
    {
           public InventoryLock(){


           }
           /// <summary>
           /// Desc:库存锁定ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long InventoryLockID {get;set;}

           /// <summary>
           /// Desc:库存ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? InventoryListID {get;set;}

           /// <summary>
           /// Desc:库存锁定类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryLockType {get;set;}

           /// <summary>
           /// Desc:库存锁定数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? InventoryLockNum {get;set;}

           /// <summary>
           /// Desc:托盘明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? TrayDetailID {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

           /// <summary>
           /// Desc:波次明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? WavePickingDetailID {get;set;}

           /// <summary>
           /// Desc:拣货明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? PickingTaskDetailID {get;set;}

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
