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

namespace AppResource.CustomControls
{
    /// <summary>
    /// ExTabItem.xaml 的交互逻辑
    /// </summary>
    public partial class ExTabItem : UserControl
    {
        int currentSelectedIndex;//当前选中tab
        int previewSelectedIndex;//上一次选中tab
        TranslateTransform translateTransform;
        DoubleAnimation daX;

        public ExTabItem()
        {
            InitializeComponent();
            previewSelectedIndex = MyTabControl.SelectedIndex;
            translateTransform = new TranslateTransform();
            foreach(var item in MyTabControl.Items)
            {
                if(item is TabItem)
                {
                    (item as TabItem).Loaded += TabItem_Loaded;
                }
            }
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabItem_Initialized(object sender, EventArgs e)
        {

        }

        private void MyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = sender as TabControl;
            ((temp.SelectedItem as TabItem).Content as Grid).RenderTransform = translateTransform;
            daX = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(500),
                From = (temp.SelectedIndex - previewSelectedIndex) * 150,
                To = 0
            };
            previewSelectedIndex = temp.SelectedIndex;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, daX);
        }
    }
}
