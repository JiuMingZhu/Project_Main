using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace UMCSearch
{
    public class SearchViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        public SearchViewModel()
        {
            if (IsInDesignModeStatic)
            {
            }
            else
            {
                click = new GalaSoft.MvvmLight.Command.RelayCommand<string>(BtnClick);
            }
        }
        private  GalaSoft.MvvmLight.Command.RelayCommand<string> click;

        public  GalaSoft.MvvmLight.Command.RelayCommand<string> Click
        {
            get { return click; }
            set { click = value; }
        }

        void BtnClick(string data)
        {
            MessageBox.Show("成功加载UserControl，ViewModel可用");
        }
    }
}
