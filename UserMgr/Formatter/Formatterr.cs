using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Entities.View;
using UserMgr.Models;

namespace UserMgr.Formatter
{
    public static class Formatterr
    {
        /// <summary>
        /// 实体类转换为视图类-父类转换为子类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TV ConvertToViewModel<TV, T>(object entity) where T : class, new() where TV : class, new()
        {
            TV model = new TV();

            foreach (var item in typeof(T).GetProperties())
            {
                try
                {
                    item.SetValue(model, item.GetValue(entity));
                }
                catch
                {
                    throw;
                }
            }
            return model;
        }



        /// <summary>
        /// 获取入库任务细节视图模型
        /// </summary>
        /// <param name="inboundTask"></param>
        /// <returns></returns>
        public static InboundTaskDetailViewModel GetInboundTaskDetailViewModel(InboundTaskDetail inboundTask)
        {
            InboundTaskDetailViewModel inboundTaskDetail = ConvertToViewModel<InboundTaskDetailViewModel, InboundTaskDetail>(inboundTask);
            inboundTaskDetail.InboundTask = new DbEntities<View_InboundTask>().SimpleClient.GetSingle(ib => ib.InboundTaskID == inboundTask.InboundTaskID);
            inboundTaskDetail.InboundTaskDetail = new DbEntities<View_InboundTaskDetail>().SimpleClient.GetSingle(ib => ib.InboundTaskDetailID == inboundTask.InboundTaskDetailID);

            return inboundTaskDetail;
        }
    }
}