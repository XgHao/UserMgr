using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TrayDetail")]
    public partial class TrayDetail
    {
           public TrayDetail(){


           }
           /// <summary>
           /// Desc:托盘明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long TrayDetailID {get;set;}

           /// <summary>
           /// Desc:入库单明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InboundTaskDetailID {get;set;}

           /// <summary>
           /// Desc:托盘编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrayNo {get;set;}

           /// <summary>
           /// Desc:组盘顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? GroupTrayOrder {get;set;}

           /// <summary>
           /// Desc:物资编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? MaterialCode {get;set;}

           /// <summary>
           /// Desc:小件数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ParcelMeasure {get;set;}

           /// <summary>
           /// Desc:托盘条码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrayCode {get;set;}

           /// <summary>
           /// Desc:容器号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ContainerNo {get;set;}

           /// <summary>
           /// Desc:重量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Weight {get;set;}

           /// <summary>
           /// Desc:物料SN
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MaterialSN {get;set;}

           /// <summary>
           /// Desc:物料版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MaterialVersion {get;set;}

           /// <summary>
           /// Desc:入库过账标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InboundPostMark {get;set;}

           /// <summary>
           /// Desc:出库过账标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OutboundPostMark {get;set;}

           /// <summary>
           /// Desc:生产日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ProductionDate {get;set;}

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
