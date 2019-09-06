using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace UserMgr.Entities.View
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("View_Material")]
    public partial class View_Material
    {
        public View_Material()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int MaterialSizeID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MaterialType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SizeCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Detail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Unit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LWH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? Weight { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MaterialModel { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ParcelUnit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParcelMeasure { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MaterialMin { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MaterialMax { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ContainerName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? IsCKD { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MaterialDensity { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? DataVersion { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CreaterName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ChangerName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ChangeTime { get; set; }

    }
}
