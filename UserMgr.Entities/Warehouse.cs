﻿using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Warehouse")]
    public partial class Warehouse
    {
           public Warehouse(){


           }
           /// <summary>
           /// Desc:仓库ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int WarehouseID {get;set;}

           /// <summary>
           /// Desc:仓库编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WarehouseNo {get;set;}

           /// <summary>
           /// Desc:仓库名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WarehouseName {get;set;}

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
           public string WarehouseType {get;set;}

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
           public DateTime? ChangeTime {get;set;}

           /// <summary>
           /// Desc:数据版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataVersion {get;set;}

    }
}