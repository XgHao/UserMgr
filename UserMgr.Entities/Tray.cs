using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tray")]
    public partial class Tray
    {
           public Tray(){


           }
           /// <summary>
           /// Desc:托盘ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long TrayID {get;set;}

           /// <summary>
           /// Desc:托盘类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrayType {get;set;}

           /// <summary>
           /// Desc:托盘编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TrayNo {get;set;}

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
           public string Container {get;set;}

           /// <summary>
           /// Desc:重量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Weight {get;set;}

           /// <summary>
           /// Desc:高度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Height {get;set;}

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
           /// Desc:入库时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? InboundTime {get;set;}

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
