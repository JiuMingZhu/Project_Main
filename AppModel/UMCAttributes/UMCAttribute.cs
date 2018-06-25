using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppModel.UMCAttributes
{
    public enum FuncType
    {
        /// <summary>
        /// 界面
        /// </summary>
        UI,
        /// <summary>
        /// 功能：播放
        /// </summary>
        Func
    }
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class UMCAttribute : Attribute
    {
        public UMCAttribute()
        {
            //构造函数
        }
        public UMCAttribute(FuncType currentType, Type classtype, string key)
        {
            //构造函数
            this.currentType = currentType;
            this.classType = classtype;
            this.key = key;
        }

        private string key;
        /// <summary>
        /// 业务编号
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private Type classType;

        public Type ClassType
        {
            get { return classType; }
            set { classType = value; }
        }

        private FuncType currentType;
        /// <summary>
        /// 业务类型
        /// </summary>
        public FuncType CurrentType
        {
            get { return currentType; }
            set { currentType = value; }
        }

    }
}
