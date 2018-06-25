using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppResourceManager;
using AppModel.FuncBase;

namespace UMCMain
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl page;
        public MainWindow()
        {
            InitializeComponent();
            UMCMainViewMode viewModel = this.Root_Grid.DataContext as UMCMainViewMode;
            Closing += viewModel.ClosingCommandFunc;
            AppResourceManager.Manager.initManager();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<FuncBase>(this, NavigateTo);            
            page = new UMCMusic.Music() as UserControl;
            Inner.Child = page;
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                DoubleAnimation widthAnimotion = new DoubleAnimation();
                DoubleAnimation heightAnimotion = new DoubleAnimation();
                widthAnimotion.From = button.ActualWidth;
                widthAnimotion.To = button.ActualWidth - 3;
                widthAnimotion.Duration = TimeSpan.FromSeconds(0.1);

                heightAnimotion.From = button.ActualHeight;
                heightAnimotion.To = button.ActualHeight - 3;
                heightAnimotion.Duration = TimeSpan.FromSeconds(0.1);
                button.BeginAnimation(Button.WidthProperty, widthAnimotion);
                button.BeginAnimation(Button.HeightProperty, heightAnimotion);
            }
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                DoubleAnimation widthAnimotion = new DoubleAnimation();
                DoubleAnimation heightAnimotion = new DoubleAnimation();
                widthAnimotion.From = button.ActualWidth;
                widthAnimotion.To = button.ActualWidth + 3;
                widthAnimotion.Duration = TimeSpan.FromSeconds(0.1);

                heightAnimotion.From = button.ActualHeight;
                heightAnimotion.To = button.ActualHeight + 3;
                heightAnimotion.Duration = TimeSpan.FromSeconds(0.1);
                button.BeginAnimation(Button.WidthProperty, widthAnimotion);
                button.BeginAnimation(Button.HeightProperty, heightAnimotion);
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //如何把这个事件放到ViewModel里面？？？
            Button btn = sender as Button;
            btn.Focus();
        }

        public void NavigateTo(FuncBase targetFunc)
        {
            try
            {
                page = System.Activator.CreateInstance(targetFunc.Type) as UserControl;
                Inner.Child = page;
            }
            catch (Exception ex)
            {
                //增加个报错通用的页面
                AppResourceManager.Manager.Loghelper.WriteLogException(ex);
            }
        }      
    }
}
