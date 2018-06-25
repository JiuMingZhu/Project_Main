using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.Message
{
    public class HttpMessage:MessageBase
    {
        private int timeOut;
        /// <summary>
        /// 超时
        /// </summary>
        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }
        private string inParam;
        /// <summary>
        /// 入参
        /// </summary>
        public string InParam
        {
            get { return inParam; }
            set { inParam = value; }
        }
        private string outParam;
        /// <summary>
        /// 出参
        /// </summary>
        public string OutParam
        {
            get { return outParam; }
            set { outParam = value; }
        }
        private string funcName;
        /// <summary>
        /// 方法名
        /// </summary>
        public string FuncName
        {
            get { return funcName; }
            set { funcName = value; }
        }

        public HttpMessage()
        {
            TimeOut = 30;
        }

        public HttpMessage(string senderToken,string funcName)
        {
            this.funcName = funcName;
            this.senderToken = senderToken;
            TimeOut = 60;
        }
    }
}
