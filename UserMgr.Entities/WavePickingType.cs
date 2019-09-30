using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WavePickingType")]
    public partial class WavePickingType
    {
           public WavePickingType(){


           }
           /// <summary>
           /// Desc:波次类型ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int WavePickingTypeID {get;set;}

           /// <summary>
           /// Desc:波次类型名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WavePickingTypeName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Other {get;set;}

    }
}
