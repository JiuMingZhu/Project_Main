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
                clickCommand = new GalaSoft.MvvmLight.Command.RelayCommand<string>(BtnClick);
            }
        }
        private  GalaSoft.MvvmLight.Command.RelayCommand<string> clickCommand;

        public  GalaSoft.MvvmLight.Command.RelayCommand<string> ClickCommand
        {
            get { return clickCommand; }
            set { clickCommand = value; }
        }


        void BtnClick(string data)
        {
            MessageBox.Show("成功加载UserControl，绑定成功！收到的参数为"+(data as  string));
        }

        void LoadedCommand(object sender, EventArgs e)
        {

        }
    }
}
