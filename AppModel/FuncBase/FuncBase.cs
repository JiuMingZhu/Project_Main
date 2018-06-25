using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.FuncBase
{
    public class FuncBase
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

        private Type type;
        /// <summary>
        /// 用于保存UserControl的Type，在NavigateTo的时候生成实例
        /// </summary>
        public Type Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public virtual int Start()
        { 
            throw new Exception("Start 未覆盖");
        }
    }
}
