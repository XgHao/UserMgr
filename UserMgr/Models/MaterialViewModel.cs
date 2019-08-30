using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class MaterialViewModel : Material
    {
        [Display(Name = "单位")]
        [MaxLength(20, ErrorMessage = "单位太长了")]
        public string UnitInput { get; set; }

        [Display(Name = "小件单位")]
        [MaxLength(20, ErrorMessage = "单位太长了")]
        public string ParcelUnitInput { get; set; }

        [Required]
        [Display(Name = "是否组合商品")]
        public bool IsCKDbool { get; set; }


        /// <summary>
        /// 返回对相应的实体类
        /// </summary>
        /// <returns></returns>
        public Material ConertMaterial(int creater)
        {
            Material model = new Material
            {
                Detail = Detail,
                DataVersion = 1,
                Height = Height,
                Length = Length,
                MaterialContainer = MaterialContainer,
                MaterialDensity = MaterialDensity,
                MaterialModel = MaterialModel,
                MaterialMax = MaterialMax,
                MaterialMin = MaterialMin,
                MaterialTypeID = MaterialTypeID,
                ParcelMeasure = ParcelMeasure,
                SizeCode = SizeCode,
                Weight = Weight,
                Width = Width,
                Creater = creater,
                CreateTime = DateTime.Now,

                IsCKD = IsCKDbool,
                Unit = string.IsNullOrEmpty(UnitInput) ? Unit : UnitInput,
                ParcelUnit = string.IsNullOrEmpty(ParcelUnitInput) ? (ParcelUnit == "-1" ? null : ParcelUnit) : ParcelUnitInput
            };
            return model;
        }
    }
}