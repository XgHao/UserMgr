using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.Entities;
using UserMgr.Formatter;

namespace UserMgr.Models
{
    public class WavePickingDetailViewModel : WavePickingDetail
    {
        public string WavePickingNo { get; set; }

        /// <summary>
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        public WavePickingDetail InitAddWavePickingDetail(int creater) => Formatterr.InitAddModel<WavePickingDetail>(this, creater);
    }
}