using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Page")]
    public partial class Page
    {
           public Page(){

            this.PageClass =Convert.ToInt32("999");

           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int PageID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PageUrl {get;set;}

           /// <summary>
           /// Desc:页面访问等级
           /// Default:999
           /// Nullable:True
           /// </summary>           
           public int? PageClass {get;set;}

           /// <summary>
           /// Desc:黑名单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BlackList {get;set;}

           /// <summary>
           /// Desc:白名单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WhiteList {get;set;}

    }
}
