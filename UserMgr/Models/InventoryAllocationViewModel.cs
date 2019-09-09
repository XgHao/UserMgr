using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Entities.View;

namespace UserMgr.Models
{
    public class InventoryAllocationViewModel : InventoryAllocation
    {
        public InventoryAllocationViewModel()
        {
            InventoryAllocations = new List<V_InventoryAllocation>();
        }

        /// <summary>
        /// 库位分配列表
        /// </summary>
        public List<V_InventoryAllocation> InventoryAllocations { get; set; }
    }

    /// <summary>
    /// 库位分配卡片模型
    /// </summary>
    public class V_InventoryAllocation
    {
        /// <summary>
        /// 库位分配信息
        /// </summary>
        public InventoryAllocation InventoryAllocation { get; set; }

        /// <summary>
        /// 库位信息
        /// </summary>
        public View_InventoryLocation InventoryLocation { get; set; }

        /// <summary>
        /// 物资种类信息
        /// </summary>
        public View_MaterialType MaterialType { get; set; }
    }
}