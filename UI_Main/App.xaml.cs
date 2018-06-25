using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace UMCMain
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //关于未处理异常的处理
            Application.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            try
            {
                AppResourceManager.Manager.Loghelper.WriteLogExceptionUnhandled("未处理异常  "+e.Exception.Message);
            }
            catch
            {
                //如果这都出异常，那就不作处理了，认命
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            UMCMain.MainWindow MainWindow = new  UMCMain.MainWindow();
            MainWindow.Show();
            App.Current.MainWindow = MainWindow;
        }
    }
}
