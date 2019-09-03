﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("InboundTaskDetail")]
    public partial class InboundTaskDetail
    {
           public InboundTaskDetail(){


           }
           /// <summary>
           /// Desc:入库任务单明细ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int InboundTaskDetailID {get;set;}

           /// <summary>
           /// Desc:入库任务单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? InboundTaskID {get;set;}

           /// <summary>
           /// Desc:物资规格ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int MaterialSizeID {get;set;}

           /// <summary>
           /// Desc:物资数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MaterialNum {get;set;}

           /// <summary>
           /// Desc:重量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Weight {get;set;}

           /// <summary>
           /// Desc:批号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BatchNumber {get;set;}

           /// <summary>
           /// Desc:送货单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DeliveryUnit {get;set;}

           /// <summary>
           /// Desc:车号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CarNum {get;set;}

           /// <summary>
           /// Desc:冰衣
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Glaze {get;set;}

           /// <summary>
           /// Desc:单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Unit {get;set;}

           /// <summary>
           /// Desc:实际入库数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ActualInboundNum {get;set;}

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
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataVersion {get;set;}

    }
}
