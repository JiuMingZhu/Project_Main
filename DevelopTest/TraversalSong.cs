using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DevelopTest
{
    /// <summary>
    /// 遍历得到音乐文件（MP3）
    /// </summary>
    internal class TraversalSong
    {
        #region 私有字段
        private string _specDir;
        /// <summary>
        /// the SpecDir to Traversal
        /// </summary>
        public string SpecDir
        {
            get { return _specDir; }
            set { _specDir = value; }
        }
        #endregion
        #region 构造函数
        public TraversalSong()
        {
            this.SpecDir = AppDomain.CurrentDomain.BaseDirectory;
        }
        public TraversalSong(string specDir)
        {
            try
            {
                this._specDir = Path.GetFullPath(specDir) + "\\";
            }
            catch (Exception ex)
            {
                this._specDir = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Constructed Function TraversalSong(string specDir) has throwed an Exception,and now dir to traversal is changed to"
                +"{0}",this._specDir);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
        #region 功能函数
        public FileInfo[] Traversal()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this._specDir);
            FileInfo[] fileList=directoryInfo.GetFiles();
            FileInfo[] songList = fileList.Where<FileInfo>(t => t.Extension == ".mp3").ToArray<FileInfo>();
            return songList;
        }
        #endregion
    }
}
