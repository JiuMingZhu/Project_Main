using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppCommon.CommonFunc
{
    public class PlayFunc
    {
        #region 私有字段
        private string _specDir;
        /// <summary>
        /// Set the specDir to Traversal,Not Recommended,Use SetSpecDir(string specDir) to set specDir safety;
        /// </summary>
        public string SpecDir
        {
            get { return _specDir; }
            set { _specDir = value; }
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// Default Constructed Fuction,Set SpecDir to Traversal "AppDomain.CurrentDomain.BaseDirectory"
        /// </summary>
        public PlayFunc()
        {
            this.SpecDir = AppDomain.CurrentDomain.BaseDirectory;
        }
        #endregion
        #region 功能函数
        /// <summary>
        /// Set SpecDir to target directory in string
        /// </summary>
        /// <param name="specDir"></param>
        /// <returns>set success or failure</returns>
        public bool SetSpecDir(string specDir)
        {
            try
            {
                this._specDir = Path.GetFullPath(specDir) + "\\";
                return true;
            }
            catch (Exception ex)
            {
                this._specDir = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine("Constructed Function SetSpecDir(string specDir) has throwed an Exception,and now dir to traversal is changed to"
                + "{0}", this._specDir);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// traversal mp3 files in specDir
        /// </summary>
        /// <returns>FileInfo[] songList</returns>
        public FileInfo[] Traversal()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this._specDir);
            FileInfo[] fileList = directoryInfo.GetFiles();
            FileInfo[] songList = fileList.Where<FileInfo>(t => t.Extension == ".mp3").ToArray<FileInfo>();
            return songList;
        }
        #endregion
    }
}
