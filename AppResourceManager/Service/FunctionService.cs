using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppResourceManager.Service
{
    internal class FunctionService : ServiceBase
    {
        /// <summary>
        /// 功能IOC容器
        /// </summary>
        private GalaSoft.MvvmLight.Ioc.SimpleIoc funcIOC;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionService()
        {
            //功能IOC
            funcIOC = new GalaSoft.MvvmLight.Ioc.SimpleIoc();
            //↓注册自身，当收到对应类型的消息的时候，会触发HandleFuncion方法
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<AppModel.Message.FuncMessage>(this, HandleFunction);
        }

        /// <summary>
        /// 注入IOC
        /// </summary>
        /// <param name="FuncType"></param>
        /// <param name="FuncKey"></param>
        public void RegisterFunction(Type FuncType, string FuncKey)
        {
            AppModel.FuncBase.FuncBase temp=System.Activator.CreateInstance(FuncType) as AppModel.FuncBase.FuncBase;
            funcIOC.Register<AppModel.FuncBase.FuncBase>(() => { return System.Activator.CreateInstance(FuncType) as AppModel.FuncBase.FuncBase; }, FuncKey, true);
        }

        /// <summary>
        /// 处理业务
        /// </summary>
        /// <param name="data"></param>
        public void HandleFunction(AppModel.Message.FuncMessage data)
        {
            AppModel.FuncBase.FuncBase currntFunc = funcIOC.GetInstance<AppModel.FuncBase.FuncBase>(data.Key);
            //增加子窗体导航
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<AppModel.FuncBase.FuncBase>(currntFunc);
            //调用Func的Start方法
            currntFunc.Start();
        }
    }
}
