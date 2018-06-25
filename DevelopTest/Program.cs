using AppCommon.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for(double i =0;i<1000;i++)
            {
                for(double j=0; j <1000; j+=0.001)
                {
                    double y = calculate(j, 45, 10);
                    Console.WriteLine(y);
                    if (y < 0)
                        Console.Read();
                }
            }
            Console.Read();
        }
        /// <summary>
        /// 斜抛曲线计算公式
        /// </summary>
        /// <param name="x">水平位置</param>
        /// <param name="xita">斜抛角度</param>
        /// <param name="v">初始速度</param>
        /// <returns></returns>
        static double calculate(double x,double xita,double v)
        {
            const double G = 9.8;
            return (x * Math.Tan(Math.PI / 180 * xita)) - G * Math.Pow(x, 2) / (2 * Math.Pow(v * Math.Cos(Math.PI / 180 * xita), 2));
        }
    }
}
