using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class SupplierViewModel : Supplier
    {
        public Supplier ConvertToSupplier(int? curUserID)
        {
            return new Supplier
            {
                SupplierName = SupplierName,
                SupplierNo = SupplierNo,
                SupplierEmail = SupplierEmail,
                SupplierPhoNum = SupplierPhoNum,
                SupplierRemark = SupplierRemark,
                Creater = curUserID,
                CreateTime = DateTime.Now
            };
        }
    }
}