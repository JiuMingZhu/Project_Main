using AppModel.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DownloadTest
{
    public class DownLoadFunc
    {
        #region constructedfunctions
        /// <summary>
        /// default onstructedfunction
        /// </summary>
        public DownLoadFunc()
        { }
        /// <summary>
        /// constructedfunction with url
        /// </summary>
        /// <param name="url">download url</param>
        public DownLoadFunc(string url)
        {
            this._url = url;
        }
        #endregion
        #region variables
        private string _url;
        /// <summary>
        /// Set download url
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        #endregion
        #region functions
        public MessageBase DownLoad()
        {
            if (string.IsNullOrEmpty(_url))
            {
                return new MessageBase(0, "Ilegal download_url ,null or empty");
            }
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_url);
                request.ContentType = "application/octet-stream";
                request.Method = "GET";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                //Get FileName
                string fileName = response.Headers["Content-Disposition"];
                fileName = Regex.Split(fileName, "filename=", RegexOptions.IgnoreCase)[1];
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    //将基础流写入内存流
                    MemoryStream memoryStream = new MemoryStream();
                    const int bufferLength = 1024;
                    int actual;
                    byte[] buffer = new byte[bufferLength];
                    //while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    //{
                    //    memoryStream.Write(buffer, 0, actual);
                    //}
                    //memoryStream.Position = 0;
                    //byte[] byt = new byte[memoryStream.Length];
                    //memoryStream.Read(byt, 0, byt.Length);
                    //memoryStream.Seek(0, SeekOrigin.Begin);
                    //bw.Write(buffer, 0, actual);
                    //bw.Close();
                    BinaryWriter bw = new BinaryWriter(File.Create(fileName));
                    while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    {
                        bw.Write(buffer, 0, actual);
                    }
                    bw.Close();
                    return new MessageBase(1, "Download Success!Path is " + AppDomain.CurrentDomain.BaseDirectory + fileName);
                }
            }
            catch (Exception ex)
            {
                return new MessageBase(0, ex.Message);
            }
        }
        public MessageBase DownLoad(string directoryPath)
        {
            if (string.IsNullOrEmpty(_url))
            {
                return new MessageBase(0, "Ilegal download_url ,null or empty");
            }
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_url);
                request.ContentType = "application/octet-stream";
                request.Method = "GET";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                //Get FileName
                string fileName = response.Headers["Content-Disposition"];
                fileName = Regex.Split(fileName, "filename=", RegexOptions.IgnoreCase)[1];
                var filesavePath = Path.Combine(directoryPath + fileName);
                BinaryWriter bw = new BinaryWriter(File.Create(filesavePath));
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    //将基础流写入内存流
                    MemoryStream memoryStream = new MemoryStream();
                    const int bufferLength = 1024;
                    int actual;
                    byte[] buffer = new byte[bufferLength];
                    //while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    //{
                    //    memoryStream.Write(buffer, 0, actual);
                    //}
                    //memoryStream.Position = 0;
                    //byte[] byt = new byte[memoryStream.Length];
                    //memoryStream.Read(byt, 0, byt.Length);
                    //memoryStream.Seek(0, SeekOrigin.Begin);
                    //bw.Write(buffer, 0, actual);
                    //bw.Close();
                    while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    {
                        bw.Write(buffer, 0, actual);
                    }
                    bw.Close();
                    return new MessageBase(1, "Download Success!Path is " + filesavePath);
                }
            }
            catch (Exception ex)
            {
                return new MessageBase(0, ex.Message);
            }
        }
        public MessageBase DownLoad(string directoryPath, string fileName)
        {
            if (string.IsNullOrEmpty(_url))
            {
                return new MessageBase(0, "Ilegal download_url ,null or empty");
            }
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_url);
                request.ContentType = "application/octet-stream";
                request.Method = "GET";
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                //Get Extensison
                string extensison = response.Headers["Content-Disposition"];
                var tempArry = Regex.Split(extensison, "filename=", RegexOptions.IgnoreCase)[1].Split('.');
                extensison = "."+tempArry[tempArry.Length - 1];
                var filesavePath = Path.Combine(directoryPath, fileName + extensison);
                BinaryWriter bw = new BinaryWriter(File.Create(filesavePath));
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    //将基础流写入内存流
                    MemoryStream memoryStream = new MemoryStream();
                    const int bufferLength = 1024;
                    int actual;
                    byte[] buffer = new byte[bufferLength];
                    //while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    //{
                    //    memoryStream.Write(buffer, 0, actual);
                    //}
                    //memoryStream.Position = 0;
                    //byte[] byt = new byte[memoryStream.Length];
                    //memoryStream.Read(byt, 0, byt.Length);
                    //memoryStream.Seek(0, SeekOrigin.Begin);
                    //bw.Write(buffer, 0, actual);
                    //bw.Close();
                    while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                    {
                        bw.Write(buffer, 0, actual);
                    }
                    bw.Close();
                    return new MessageBase(1, "Download Success!Path is " + filesavePath);
                }
            }
            catch (Exception ex)
            {
                return new MessageBase(0, ex.Message);
            }
        }
        #endregion
    }
}
