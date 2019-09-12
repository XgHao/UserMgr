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
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <returns></returns>
        public Material InitAddMaterial(int creater)
        {
            Material entity = this as Material;
            entity.IsCKD = IsCKDbool;
            entity.Unit = string.IsNullOrEmpty(UnitInput) ? Unit : UnitInput;
            entity.ParcelUnit = string.IsNullOrEmpty(ParcelUnitInput) ? (ParcelUnit == "-1" ? null : ParcelUnit) : ParcelUnitInput;
            entity.Changer = entity.Changer = creater;
            entity.ChangeTime = entity.CreateTime = DateTime.Now;
            entity.DataVersion = 1;

            return entity;
        }
    }
}