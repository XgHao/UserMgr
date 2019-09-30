using System;
using System.Linq;
using System.Text;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace UserMgr.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WavePicking")]
    public partial class WavePicking
    {
        public WavePicking()
        {


        }
        /// <summary>
        /// Desc:波次ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int WavePickingID { get; set; }

        /// <summary>
        /// Desc:波次编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [Display(Name = "波次编码")]
        [RegularExpression("^[A-Za-z0-9]{4,40}$", ErrorMessage = "编码只能由4-40个数字或字母组成")]
        public string WavePickingNo { get; set; }

        /// <summary>
        /// Desc:波次类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择波次类型")]
        public int? WavePickingTypeID { get; set; }

        /// <summary>
        /// Desc:拣货类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Required]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "请选择拣货类型")]
        public int? PickingType { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Status { get; set; }

        /// <summary>
        /// Desc:录入人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Creater { get; set; }

        /// <summary>
        /// Desc:录入时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:修改人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Changer { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }

        /// <summary>
        /// Desc:数据版本
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

    }
}
