using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WavePickingDetail")]
    public partial class WavePickingDetail
    {
           public WavePickingDetail(){


           }
           /// <summary>
           /// Desc:波次明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long WavePickingDetailID {get;set;}

           /// <summary>
           /// Desc:波次ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? WavePickingID {get;set;}

           /// <summary>
           /// Desc:出库单明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OutboundTaskDetailID {get;set;}

           /// <summary>
           /// Desc:物资规格ID
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
           /// Desc:批号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BatchNumber {get;set;}

           /// <summary>
           /// Desc:托盘明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? TrayDetailID {get;set;}

           /// <summary>
           /// Desc:库存ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryID {get;set;}

           /// <summary>
           /// Desc:冰衣
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Glaze {get;set;}

           /// <summary>
           /// Desc:单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Unit {get;set;}

           /// <summary>
           /// Desc:分配数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QuantityAllotted {get;set;}

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
