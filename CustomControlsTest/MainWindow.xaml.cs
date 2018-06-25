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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControlsTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool f = false;
        double x0;
        double x1;
        double y0;
        double y1;
        double xita = 45;
        double v = 30;
        double Y = 200;
        double X = 200;
        public MainWindow()
        {
            InitializeComponent();
            //大概就是画切线，切线画多了，曲线就出来了。
            //内存会爆炸的。。。
            //for (double i=0.1;i<1000;i+=0.2)
            //{
            //    f = !f;
            //    x0 = i-0.1;
            //    y0 = calculate(x0, xita, v);
            //    x1 = i;
            //    y1 = calculate(x1, xita, v);
            //    if(f)
            //    {
            //        Line l = new Line() { X1 = x0+X, X2 = x1+X, Y1 = Y-y0, Y2 = Y-y1,Stroke=new SolidColorBrush(Color.FromRgb(0,0,0)) };
            //        grid.Children.Add(l);
            //    }
            //}

            // Create a path to draw a geometry with.
            Path myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            List<Point> values = new List<Point>();
            for (double i = 0.1; i < 1000; i += 0.2)
            {
                f = !f;
                x0 = i - 0.1;
                y0 = calculate(x0, xita, v);
                x1 = i;
                y1 = calculate(x1, xita, v);
                values.Add(new Point(x0+X,Y- y0));
                values.Add(new Point(x1+X,Y- y1));
            }
            StreamGeometry theGeometry = BuildRegularPolygon(values, false, false);
            // Create a StreamGeometry to use to specify myPath.
            theGeometry.FillRule = FillRule.EvenOdd;

            // Freeze the geometry (make it unmodifiable)
            // for additional performance benefits.
            theGeometry.Freeze();

            // Use the StreamGeometry returned by the BuildRegularPolygon to 
            // specify the shape of the path.
            myPath.Data = theGeometry;

            // Add path shape to the UI.
            grid.Children.Add(myPath);

        }

        /// <summary>
        /// 斜抛曲线计算公式
        /// </summary>
        /// <param name="x">水平位置</param>
        /// <param name="xita">斜抛角度</param>
        /// <param name="v">初始速度</param>
        /// <returns></returns>
        double calculate(double x, double xita, double v)
        {
            const double G = 9.8;
            return (x * Math.Tan(Math.PI / 180 * xita)) - G * Math.Pow(x, 2) / (2 * Math.Pow(v * Math.Cos(Math.PI / 180 * xita), 2));
        }

        /// <summary>
        /// 绘制连续的线段
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private StreamGeometry BuildRegularPolygon(List<Point> values, bool isClosed, bool isfilled)
        {
            // c is the center, r is the radius,
            // numSides the number of sides, offsetDegree the offset in Degrees.
            // Do not add the last point.

            StreamGeometry geometry = new StreamGeometry();

            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(values[0], isfilled /* is filled */, isClosed /* is closed */);

                for (int i = 1; i < values.Count; i++)
                {
                    ctx.LineTo(values[i], true /* is stroked */, false /* is smooth join */);
                }
            }

            return geometry;

        }
    }
}
