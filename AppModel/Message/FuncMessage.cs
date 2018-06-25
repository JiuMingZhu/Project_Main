using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.Message
{
    public class FuncMessage:MessageBase
    {
        public FuncMessage(string senderToken)
        {
            this.senderToken = senderToken;
        }
        private string key;
        /// <summary>
        /// FuncKey
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

    }
}
