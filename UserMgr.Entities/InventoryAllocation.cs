using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InventoryAllocation")]
    public partial class InventoryAllocation
    {
           public InventoryAllocation(){


           }
           /// <summary>
           /// Desc:库位分配ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int InventoryAllocationID {get;set;}

           /// <summary>
           /// Desc:物资种类ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MaterialTypeID {get;set;}

           /// <summary>
           /// Desc:库位ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InventoryLocationID {get;set;}

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
