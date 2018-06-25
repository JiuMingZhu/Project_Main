using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomControlsTest
{
    class MainWindowVM : GalaSoft.MvvmLight.ViewModelBase
    {
        #region 构造函数
        public MainWindowVM()
        {
            if (IsInDesignModeStatic)
            {
                //ToDo
                my_Text = "测试环境";
            }
            else
            {
                //ToDo
                my_Text = "正式环境_资源字典";
            }
        }
        #endregion
        #region
        private string my_Text;

        public string My_Text
        {
            get { return my_Text; }
            set
            {
                my_Text = value;
                RaisePropertyChanged("My_Text");
            }
        }

        #endregion
    }
}
