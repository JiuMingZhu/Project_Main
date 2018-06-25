using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AppResourceManager.Service
{
    public class HttpService
    {
        public void HttpPost(AppModel.Message.HttpMessage Data)
        {
            byte[] toPost = Encoding.UTF8.GetBytes(Data.InParam);
            string requestUrl = "http://localhost:8088/Project_Main/" + Data.FuncName;
            Uri requestUri = new Uri(requestUrl);//转换成URI
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.ReadWriteTimeout = 5000;//读写流超时5s
            request.Timeout = 30 * 1000;//超时30s
            request.Method = "Post";
            request.ContentType = "application/json";
            request.CookieContainer = null;//这个删了应该也没影响吧
            string responseString;//响应消息
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(toPost, 0, toPost.Length);
                requestStream.Flush();
                requestStream.Close();
            }
            using (Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream())
            {
                StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
                responseString = sr.ReadToEnd();
                responseStream.Close();
                sr.Close();
            }
        }
    }
}
