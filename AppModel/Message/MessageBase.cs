using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.Message
{
    public class MessageBase
    {
        public MessageBase() { }
        public MessageBase(int rspCode, string rspMsg) { this.rspCode = rspCode; this.rspMsg = rspMsg; }
        //发送源Token
        public string senderToken { get; set; }
        //返回状态码
        public int rspCode { get; set; }
        //返回状态消息
        public string rspMsg { get; set; }
    }

}
