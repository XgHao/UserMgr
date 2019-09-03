using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("OutboundTask")]
    public partial class OutboundTask
    {
           public OutboundTask(){


           }
           /// <summary>
           /// Desc:出库任务单ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int OutboundTaskID {get;set;}

           /// <summary>
           /// Desc:出库任务单编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OutboundTaskNo {get;set;}

           /// <summary>
           /// Desc:出库类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OutboundType {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OutboundRemark {get;set;}

           /// <summary>
           /// Desc:客户
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Client {get;set;}

           /// <summary>
           /// Desc:外部单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ExterNo {get;set;}

           /// <summary>
           /// Desc:销售单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SaleNo {get;set;}

           /// <summary>
           /// Desc:部门
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Department {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

           /// <summary>
           /// Desc:任务完成时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? TaskCompletionTime {get;set;}

           /// <summary>
           /// Desc:录入人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Creater {get;set;}

           /// <summary>
           /// Desc:录入时间
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
