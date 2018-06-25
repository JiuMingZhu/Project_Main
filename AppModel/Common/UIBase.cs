using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.Common
{
    public class UIBase
    {
        private string name;
        /// <summary>
        /// 功能名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string key;
        /// <summary>
        /// 功能Key
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        public virtual int Start()
        {
            //成功返回0
            throw new Exception("未实现Start方法");
        }
    }
}
