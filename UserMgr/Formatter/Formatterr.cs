﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserMgr.DB;
using UserMgr.Entities;
using UserMgr.Entities.View;
using UserMgr.Models;
using System.Reflection;

namespace UserMgr.Formatter
{
    /// <summary>
    /// 模型转换
    /// </summary>
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
        /// 添加新实体-初始化某些数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tV"></param>
        /// <param name="creater"></param>
        /// <returns></returns>
        public static T InitAddModel<T>(object tV, int creater) where T : class, new()
        {
            T entity;
            try
            {
                entity = tV as T;
            }
            catch { throw; }

            DateTime time = DateTime.Now;

            Type typeT = typeof(T);
            typeT.GetProperty("Creater").TrySetValue(entity, creater);
            typeT.GetProperty("Changer").TrySetValue(entity, creater);
            typeT.GetProperty("CreateTime").TrySetValue(entity, time);
            typeT.GetProperty("ChangeTime").TrySetValue(entity, time);
            typeT.GetProperty("Status").TrySetValue(entity, 1);
            typeT.GetProperty("DataVersion").TrySetValue(entity, 1);

            return entity;
        }



        /// <summary>
        /// 获取入库任务细节视图模型
        /// </summary>
        /// <param name="inboundTask"></param>
        /// <returns></returns>
        public static InboundTaskDetailViewModel GetInboundTaskDetailViewModel(InboundTaskDetail inboundTask)
        {
            InboundTaskDetailViewModel inboundTaskDetail = ConvertToViewModel<InboundTaskDetailViewModel, InboundTaskDetail>(inboundTask);

            //入库任务信息
            inboundTaskDetail.InboundTask = new DbEntities<View_InboundTask>().SimpleClient.GetSingle(ib => ib.InboundTaskID == inboundTask.InboundTaskID);

            //入库任务细节信息
            inboundTaskDetail.InboundTaskDetail = new DbEntities<View_InboundTaskDetail>().SimpleClient.GetSingle(ibd => ibd.InboundTaskDetailID == inboundTask.InboundTaskDetailID);

            return inboundTaskDetail;
        }


        /// <summary>
        /// 获取出库任务细节视图模型
        /// </summary>
        /// <param name="outboundTask"></param>
        /// <returns></returns>
        public static OutboundTaskDetailViewModel GetOutboundTaskDetailViewModel(OutboundTaskDetail outboundTask)
        {
            OutboundTaskDetailViewModel outboundTaskDetail = ConvertToViewModel<OutboundTaskDetailViewModel, OutboundTaskDetail>(outboundTask);

            //出库任务信息
            outboundTaskDetail.OutboundTask = new DbEntities<View_OutboundTask>().SimpleClient.GetSingle(ob => ob.OutboundTaskID == outboundTask.OutboundTaskID);

            //出库任务细节信息
            outboundTaskDetail.OutboundTaskDetail = new DbEntities<View_OutboundTaskDetail>().SimpleClient.GetSingle(obd => obd.OutboundTaskDetailID == outboundTask.OutboundTaskDetailID);

            return outboundTaskDetail;
        }
    }
}