using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PickingType")]
    public partial class PickingType
    {
        public PickingType()
        {
            IsAbandon = false;
        }
        /// <summary>
        /// Desc:拣货类型ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int PickingTypeID { get; set; }

        /// <summary>
        /// Desc:拣货类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PickingName { get; set; }

        /// <summary>
        /// Desc:抛弃标识
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public bool IsAbandon { get; set; }

        /// <summary>
        /// Desc:预留字段
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Other { get; set; }

    }
}
