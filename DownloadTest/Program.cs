using AppModel.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DownloadTest
{
    class Program
    {
        static string url = "https://github.com/shadowsocks/shadowsocks-windows/releases/download/4.0.7/Shadowsocks-4.0.7.zip";
        static void Main(string[] args)
        {
            DownLoadFunc dl = new DownLoadFunc("http://blog.csdn.net/luanpeng825485697/article/details/78165813");
            MessageBase ret = dl.DownLoad();
            Console.WriteLine(ret.rspMsg);
            //ret = dl.DownLoad(@"asdsa\");
            //Console.WriteLine(ret.rspMsg);
            //ret = dl.DownLoad(@"szd\", "sss");
            //Console.WriteLine(ret.rspMsg);
            Console.Read();
        }
    }
}
