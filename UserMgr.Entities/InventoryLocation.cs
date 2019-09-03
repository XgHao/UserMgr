using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InventoryLocation")]
    public partial class InventoryLocation
    {
           public InventoryLocation(){


           }
           /// <summary>
           /// Desc:库位ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int InventoryLocationID {get;set;}

           /// <summary>
           /// Desc:库区ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryAreaID {get;set;}

           /// <summary>
           /// Desc:库位类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryLocationType {get;set;}

           /// <summary>
           /// Desc:库位编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryLocationNo {get;set;}

           /// <summary>
           /// Desc:库位名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryLocationName {get;set;}

           /// <summary>
           /// Desc:库位长度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? InventoryLocationLength {get;set;}

           /// <summary>
           /// Desc:库位高度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryLocationHeight {get;set;}

           /// <summary>
           /// Desc:库位宽度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? InventoryLocationWidth {get;set;}

           /// <summary>
           /// Desc:排
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Row {get;set;}

           /// <summary>
           /// Desc:层
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Layer {get;set;}

           /// <summary>
           /// Desc:列
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Line {get;set;}

           /// <summary>
           /// Desc:库位分组
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryLocationGroup {get;set;}

           /// <summary>
           /// Desc:库位巷道
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryLocationNarrow {get;set;}

           /// <summary>
           /// Desc:入口距离
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? EnterDistance {get;set;}

           /// <summary>
           /// Desc:出口距离
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ExitDistance {get;set;}

           /// <summary>
           /// Desc:前后
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? FrontAndBack {get;set;}

           /// <summary>
           /// Desc:容器
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Container {get;set;}

           /// <summary>
           /// Desc:是否开启
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? Enable {get;set;}

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
