using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities.View
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("View_Warehouse")]
    public partial class View_Warehouse
    {
           public View_Warehouse(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int WarehouseID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WarehouseNo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WarehouseName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool Enable {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WarehouseType {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OtherInfo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CreaterName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ChangerName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ChangeTime {get;set;}

    }
}
