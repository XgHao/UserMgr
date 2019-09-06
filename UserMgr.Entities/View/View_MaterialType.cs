using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities.View
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("View_MaterialType")]
    public partial class View_MaterialType
    {
           public View_MaterialType(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int MaterialTypeID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MaterialTypeName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MaterialTypeNo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MaterialTypeRoot {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Supplier {get;set;}

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

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataVersion {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MaterialTypePrice {get;set;}

    }
}
