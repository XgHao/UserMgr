using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Entities.View;

namespace UserMgr.Models
{
    public class InventoryAllocationViewModel
    {
        List<View_InventoryAllocation> inventoryAllocations;
    }

    public class View_InventoryAllocation
    {
        public View_InventoryLocation InventoryLocation;

        public View_MaterialType MaterialType;
    }
}