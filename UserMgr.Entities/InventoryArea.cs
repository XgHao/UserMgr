using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InventoryArea")]
    public partial class InventoryArea
    {
           public InventoryArea(){


           }
           /// <summary>
           /// Desc:库位ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int InventoryAreaID {get;set;}

           /// <summary>
           /// Desc:仓库ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? WarehouseID {get;set;}

           /// <summary>
           /// Desc:库区编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaNo {get;set;}

           /// <summary>
           /// Desc:库区名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaName {get;set;}

           /// <summary>
           /// Desc:是否开放
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? Enable {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:库区类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaType {get;set;}

           /// <summary>
           /// Desc:其他信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OtherInfo {get;set;}

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
           public DateTime? ChangeTimr {get;set;}

           /// <summary>
           /// Desc:数据版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataVersion {get;set;}

    }
}
