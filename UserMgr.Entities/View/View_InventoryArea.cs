using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities.View
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("View_InventoryArea")]
    public partial class View_InventoryArea
    {
           public View_InventoryArea(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int InventoryAreaID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Warehouse {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaNo {get;set;}

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
           public string Remark {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string InventoryAreaType {get;set;}

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
