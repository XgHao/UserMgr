using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Formatter;
using UserMgr.Entities;

namespace UserMgr.Models
{
    public class WavePickingTypeViewModel : WavePickingType
    {
        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public WavePickingType InitAddWavePickingType(int creater) => Formatterr.InitAddModel<WavePickingType>(this, creater);
    }
}