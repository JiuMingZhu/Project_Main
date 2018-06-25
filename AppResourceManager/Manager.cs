    using AppResourceManager.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppResourceManager
{
    public class Manager
    {
        public static int initManager()
        {
            bool bb=AppCommon.Log.UMCLog.Enable;//使用静态变量触发类的静态构造函数（get√）
            #region 服务注册
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<FunctionService>(true);//注入ioc，并立即实例化
            #endregion
            DirectoryInfo AppFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo NextFile in AppFolder.GetFiles())
            {
                bool b = NextFile.Name.StartsWith("UMC");
                if (NextFile.Extension.ToUpper() == ".DLL" && NextFile.Name.StartsWith("UMC"))
                {
                    Loghelper.WriteLogTrace("正在加载：" +NextFile.FullName.ToString());
                    System.Reflection.Assembly assemblyInfo = null;
                    try
                    {
                        assemblyInfo = System.Reflection.Assembly.LoadFrom(NextFile.FullName);
                    }
                    catch
                    {
                        assemblyInfo = null;
                    }
                    if (assemblyInfo == null)
                        continue;
                    //获取所有公开类型
                    Type[] TempTArray = assemblyInfo.GetExportedTypes();
                    foreach (Type type in TempTArray)
                    {
                        object[] TypeAttributes = type.GetCustomAttributes(typeof(AppModel.UMCAttributes.UMCAttribute), false);
                        if (TypeAttributes != null && TypeAttributes.Length > 0)
                        {
                            //程序集里面不是所有的公共类型都是自定义特性的，所以在此做个判断
                            try
                            {
                                //特性只有一个，所以是0
                                AppModel.UMCAttributes.UMCAttribute temp = TypeAttributes[0] as AppModel.UMCAttributes.UMCAttribute;
                                switch (temp.CurrentType)
                                {
                                    case AppModel.UMCAttributes.FuncType.Func:
                                        //↓先从容器中获取Service实例，然后调用service的自定义注入方法
                                        GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<FunctionService>().RegisterFunction(temp.ClassType,temp.Key);
                                        break;
                                    default: break;
                                }
                            }
                            catch(Exception ex)
                            { Loghelper.WriteLogException(ex);}
                        }
                    }

                }
            }
            return -1;
        }

        /// <summary>
        /// 功能消息转发
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static bool SendFuncMessage(AppModel.Message.FuncMessage Data)
        {
            if (string.IsNullOrEmpty(Data.senderToken))
            {
                return false;
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<AppModel.Message.FuncMessage>(Data);
                return true;
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        public static AppCommon.Log.UMCLog Loghelper
        {
            get
            {
                return GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<AppCommon.Log.UMCLog>();
            }
        }
        /// <summary>
        /// 发送HTTP消息
        /// </summary>
        public void SendHttpMessage(AppModel.Message.HttpMessage Data)
        {
            //没有senderToken就返回
            if (string.IsNullOrEmpty(Data.senderToken))
            {
                return;
            }

        }
    }
}
