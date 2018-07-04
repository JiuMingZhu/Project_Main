using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace UMCMain
{
    internal class UMCMainViewMode : GalaSoft.MvvmLight.ViewModelBase
    {
        #region 构造函数
        public UMCMainViewMode()
        {
            if (IsInDesignModeStatic)
            {
                Title = "扯淡云(并没有)音乐";
                CreateFaviourtes = "CreateFaviourtes";
                Img_Singer = "1";
                ReSizeBtn = "Category";
                Portrait = "Portrait";
                UserName = "晓风中的芣苢";
                Message = "Message";
                Setting = "Setting";
                //
                Cloud = "Cloud";
                CreateFaviourtes = "CreateFaviourtes";
                Downloads = "Downloads";
                Faviourtes = "Faviourtes";
                Friends = "Friends";
                Like = "LikeRed";
                LocalMusic = "LocalMusic";
                Music = "Music";
                MusicList = "MusicList";
                Radio = "Radio";
                Recent = "Recent";
                Search = "Search";
                MV = "MV";
                //
                Previous = "Previous";
                Play = "Play";
                Next = "Next";
                //
                StaticButtons = new ObservableCollection<ButtonContent>();
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Search", ButtonContent_Text = "搜索", FuncKey = "1" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Music", ButtonContent_Text = "发现音乐", FuncKey = "2" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "MV", ButtonContent_Text = "MV", FuncKey = "3" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Friends", ButtonContent_Text = "朋友", FuncKey = "4" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Friends", ButtonContent_Text = "朋友", FuncKey = "4" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Friends", ButtonContent_Text = "朋友", FuncKey = "4" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Friends", ButtonContent_Text = "朋友", FuncKey = "4" });
                //
                SongName = "伤不起";
                SingerName = "王玲";
                PlayingCurrentTime = "00:00";
                PlayingTotalTime = "03:49";
                //
                PlayMethod = "PlaySingleCycle";
                Volume = "Volume";
                PlayList = "PlayList";
            }
            else
            {
                //可以考虑放到xml文档里（反正就是配置文档）
                Title = "扯淡云(并没有)音乐";
                CreateFaviourtes = "CreateFaviourtes";
                Img_Singer = "1";
                ReSizeBtn = "Category";
                Portrait = "Portrait";
                UserName = "晓风中的芣苢";
                Message = "Message";
                Setting = "Setting";
                Previous = "Previous";
                Play = "Play";
                Next = "Next";
                PlayMethod = "PlaySingleCycle";
                Volume = "Volume";
                Like = "LikeRed";
                PlayList = "PlayList";
                StaticButtons = new ObservableCollection<ButtonContent>();
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Search", ButtonContent_Text = "搜索", FuncKey = "1" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Music", ButtonContent_Text = "发现音乐", FuncKey = "2" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "MV", ButtonContent_Text = "MV", FuncKey = "3" });
                StaticButtons.Add(new ButtonContent() { ButtonContent_Img = "Friends", ButtonContent_Text = "朋友", FuncKey = "4" });
                titleButton_Click = new GalaSoft.MvvmLight.Command.RelayCommand<string>(TitleButtonFunc);
                listButton_Click = new GalaSoft.MvvmLight.Command.RelayCommand<string>(ListButtonFunc);
                previousSong = new GalaSoft.MvvmLight.Command.RelayCommand(PreviousSongFunc);
                playSong = new GalaSoft.MvvmLight.Command.RelayCommand(PlaySongFunc);
                nextSong = new GalaSoft.MvvmLight.Command.RelayCommand(NextSongFunc);
                wo = new NAudio.Wave.WaveOut(NAudio.Wave.WaveCallbackInfo.FunctionCallback());
                wo.PlaybackStopped += PlaybackStopped;
                songList = playEntity.Traversal();
                getCurrrentTime_th = new Thread(() =>
                {
                    while (!Cancel)
                    {
                        PlayingCurrentTime = mp3FileReader.CurrentTime.ToString().Substring(0, 8);
                        Thread.Sleep(500);
                    }
                });
            }
        }
        #endregion
        #region 字段定义
        #region 系统字段
        private ObservableCollection<ButtonContent> staticButtons;
        /// <summary>
        /// 按钮列表
        /// </summary>
        public ObservableCollection<ButtonContent> StaticButtons
        {
            get { return staticButtons; }
            set { staticButtons = value; }
        }

        #region 图标资源
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 专辑封面
        /// </summary>
        public string Img_Singer { get; set; }
        /// <summary>
        /// 左边栏缩小按钮
        /// </summary>
        public string ReSizeBtn { get; set; }
        /// <summary>
        /// 我的音乐云盘
        /// </summary>
        public string Cloud { get; set; }
        /// <summary>
        /// 创建歌单
        /// </summary>
        public string CreateFaviourtes { get; set; }
        /// <summary>
        /// 下载管理
        /// </summary>
        public string Downloads { get; set; }
        /// <summary>
        /// 我的收藏
        /// </summary>
        public string Faviourtes { get; set; }
        /// <summary>
        /// 朋友
        /// </summary>
        public string Friends { get; set; }
        /// <summary>
        /// 我喜欢的音乐
        /// </summary>
        public string Like { get; set; }
        /// <summary>
        /// 本地音乐
        /// </summary>
        public string LocalMusic { get; set; }
        /// <summary>
        /// 发现音乐
        /// </summary>
        public string Music { get; set; }
        /// <summary>
        /// 歌单名前面的图标
        /// </summary>
        public string MusicList { get; set; }
        /// <summary>
        /// MV
        /// </summary>
        public string MV { get; set; }
        /// <summary>
        /// 我的电台
        /// </summary>
        public string Radio { get; set; }
        /// <summary>
        /// 最近播放
        /// </summary>
        public string Recent { get; set; }
        /// <summary>
        /// 搜索音乐
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Portrait { get; set; }
        /// <summary>
        /// 通知
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 设置
        /// </summary>
        public string Setting { get; set; }
        /// <summary>
        /// 上一曲
        /// </summary>
        public string Previous { get; set; }
        private string play;
        /// <summary>
        /// 播放\暂停
        /// </summary>
        public string Play
        {
            get
            {
                return play;
            }
            set
            {
                play = value;
                RaisePropertyChanged("Play");
            }
        }
        /// <summary>
        /// 下一曲
        /// </summary>
        public string Next { get; set; }
        #endregion
        #endregion
        #region 用户字段
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;
        public string UserName
        {
            get
            {
                if (userName.Length <= 4)
                    return userName;
                return userName.Substring(0, 3) + "...";
            }
            set
            {
                if (value is string)
                    userName = value;
            }
        }


        #endregion
        #region 播放器功能字段
        /// <summary>
        /// 播放功能实体
        /// </summary>
        private AppCommon.CommonFunc.PlayFunc playEntity = new AppCommon.CommonFunc.PlayFunc();
        NAudio.Wave.Mp3FileReader mp3FileReader;
        NAudio.Wave.WaveOut wo;
        //是否停止后台线程
        bool Cancel { get; set; }
        //查询线程，同步歌曲播放时间
        Thread getCurrrentTime_th;
        //标志是否由上一曲、下一曲触发的Stop事件，避免重复触发，导致线程出错
        bool isCausedByFunc;
        /// <summary>
        /// 是否正在播放
        /// </summary>
        private bool IsPlaying { get; set; }
        /// <summary>
        /// 是否已经播放
        /// </summary>
        private bool IsPlayed { get; set; }
        /// <summary>
        /// 正在播放的歌曲在列表中的序号
        /// </summary>
        public int PlayingIndex { get; set; }
        /// <summary>
        /// 播放列表
        /// </summary>
        private System.IO.FileInfo[] songList;
        private string songName;
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string SongName
        {
            get { return songName; }
            set
            {
                songName = value;
                RaisePropertyChanged("SongName");
            }
        }

        private string singerName;
        /// <summary>
        /// 歌手名
        /// </summary>
        public string SingerName
        {
            get { return singerName; }
            set
            {
                singerName = value;
                RaisePropertyChanged("SingerName");
            }
        }


        private string playingCurrentTime;
        /// <summary>
        /// 歌曲当前播放进度
        /// </summary>
        public string PlayingCurrentTime
        {
            get { return playingCurrentTime; }
            set
            {
                playingCurrentTime = value;
                RaisePropertyChanged("PlayingCurrentTime");
            }
        }

        private string playingTotalTime;
        /// <summary>
        /// 歌曲总时长
        /// </summary>
        public string PlayingTotalTime
        {
            get { return playingTotalTime; }
            set
            {
                playingTotalTime = value;
                RaisePropertyChanged("PlayingTotalTime");
            }
        }

        /// <summary>
        /// 播放方式
        /// </summary>
        public string PlayMethod { get; set; }
        /// <summary>
        /// 音量
        /// </summary>
        public string Volume { get; set; }
        /// <summary>
        /// 播放列表
        /// </summary>
        public string PlayList { get; set; }
        #endregion
        #endregion
        #region 事件处理
        #region 窗体
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        public void ClosingCommandFunc(object sender, CancelEventArgs e) 
        {
            if (wo != null)
            {
                wo.Stop();
                wo.Dispose();
            }
            if (mp3FileReader != null)
            {
                mp3FileReader.Close();
                mp3FileReader.Dispose();
            }
            if (getCurrrentTime_th != null)
            {
                if ((getCurrrentTime_th.ThreadState & ThreadState.Running) == ThreadState.Running || (getCurrrentTime_th.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
                {
                    getCurrrentTime_th.Abort();
                }
            }
            System.Environment.Exit(0);
        }
        #endregion
        #region 标题栏
        private GalaSoft.MvvmLight.Command.RelayCommand<string> titleButton_Click;
        /// <summary>
        /// 标题栏按钮事件
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand<string> TitleButton_Click
        {
            get { return titleButton_Click; }
            set { titleButton_Click = value; }
        }
        void TitleButtonFunc(string Data)
        {
            Window CurrentWindow = Application.Current.MainWindow;
            switch (Data)
            {
                case "Min":
                    CurrentWindow.WindowState = WindowState.Minimized;
                    break;
                case "Max":
                    if (CurrentWindow.WindowState == WindowState.Maximized)
                        CurrentWindow.WindowState = WindowState.Normal;
                    else
                        CurrentWindow.WindowState = WindowState.Maximized;
                    break;
                case "Close":
                    CurrentWindow.Close();
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region 功能列表点击
        private GalaSoft.MvvmLight.Command.RelayCommand<string> listButton_Click;
        /// <summary>
        /// 列表按钮点击事件
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand<string> ListButton_Click
        {
            get { return listButton_Click; }
            set { listButton_Click = value; }
        }
        void ListButtonFunc(string Data)
        {
            switch (Data)
            {
                default:
                    AppResourceManager.Manager.SendFuncMessage(new AppModel.Message.FuncMessage(this.GetType().FullName) { Key = Data });
                    break;
            }
        }
        #endregion
        #region 播放器控制
        private GalaSoft.MvvmLight.Command.RelayCommand previousSong;
        /// <summary>
        /// 上一曲
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand PreviousSong
        {
            get { return previousSong; }
            set { previousSong = value; }
        }
        private GalaSoft.MvvmLight.Command.RelayCommand playSong;
        /// <summary>
        /// 播放、暂停
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand PlaySong
        {
            get { return playSong; }
            set { playSong = value; }
        }
        private GalaSoft.MvvmLight.Command.RelayCommand nextSong;
        /// <summary>
        /// 下一曲
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand NextSong
        {
            get { return nextSong; }
            set { nextSong = value; }
        }
        void PreviousSongFunc()
        {
            isCausedByFunc = true;
            songList = playEntity.Traversal();
            if (songList != null && songList.Length != 0)
            {
                Play = "Pause";
                PlayingIndex = (PlayingIndex + songList.Length - 1) % songList.Length;
                wo.Dispose();
                mp3FileReader = new NAudio.Wave.Mp3FileReader(songList[PlayingIndex].FullName);
                PlayingTotalTime = mp3FileReader.TotalTime.ToString().Substring(0, 8);
                string[] temp = songList[PlayingIndex].Name.Split('-');
                SingerName = temp[0];
                SongName = temp[1];
                wo.Init(mp3FileReader);
                wo.Play();
                if ((getCurrrentTime_th.ThreadState & ThreadState.Unstarted) == ThreadState.Unstarted)
                    getCurrrentTime_th.Start();
                IsPlayed = true;
                IsPlaying = true;
            }
        }
        void PlaySongFunc()
        {
            if (!IsPlaying)
            {
                Play = "Pause";
                IsPlaying = !IsPlaying;
                if (songList != null && songList.Length != 0)
                {
                    if (!IsPlayed)
                    {
                        string[] temp = songList[PlayingIndex].Name.Split('-');
                        SingerName = temp[0];
                        SongName = temp[1];
                        mp3FileReader = new NAudio.Wave.Mp3FileReader(songList[PlayingIndex].FullName);
                        PlayingTotalTime = mp3FileReader.TotalTime.ToString().Substring(0, 8);
                        wo.Init(mp3FileReader);
                        getCurrrentTime_th.Start();
                        wo.Play();
                        IsPlayed = true;

                    }
                    else
                    {
                        wo.Resume();
                        if (getCurrrentTime_th != null && (getCurrrentTime_th.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
                        {
                            getCurrrentTime_th.Resume();
                        }
                    }
                }
            }
            else
            {
                Play = "Play";
                IsPlaying = !IsPlaying;
                if (getCurrrentTime_th != null && (getCurrrentTime_th.ThreadState&ThreadState.Running) == ThreadState.Running)
                {
                    getCurrrentTime_th.Suspend();
                }
                wo.Pause();
            }

        }
        void NextSongFunc()
        {
            //if (getCurrrentTime_th != null && (getCurrrentTime_th.ThreadState & ThreadState.Running )== ThreadState.Running)
            //{
            //        getCurrrentTime_th.Suspend();
            //}
            isCausedByFunc = true;
            if (songList != null && songList.Length != 0)
            {
                Play = "Pause";
                PlayingIndex = (PlayingIndex + songList.Length + 1) % songList.Length;
                wo.Stop();
                wo.Dispose();
                mp3FileReader = new NAudio.Wave.Mp3FileReader(songList[PlayingIndex].FullName);
                PlayingTotalTime = mp3FileReader.TotalTime.ToString().Substring(0, 8);
                string[] temp = songList[PlayingIndex].Name.Split('-');
                SingerName = temp[0];
                SongName = temp[1];
                wo.Init(mp3FileReader);
                wo.Play();
                //Thread.Sleep(500);
                //if (getCurrrentTime_th != null && (getCurrrentTime_th.ThreadState & ThreadState.Suspended) == ThreadState.Suspended)
                //{
                //    getCurrrentTime_th.Resume();
                //}
                if ((getCurrrentTime_th.ThreadState & ThreadState.Unstarted) == ThreadState.Unstarted)
                    getCurrrentTime_th.Start();
                IsPlayed = true;
                IsPlaying = true;
            }
        }
        private void PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            //播放完当前歌曲，播放下一首
            if (isCausedByFunc)
                isCausedByFunc = false;
            else
            {
                NextSongFunc();
                isCausedByFunc = false;
            }
        }
        public void Stopped()
        {
            this.Cancel = true;
            wo.Stop();
            wo.Dispose();
            mp3FileReader.Dispose();
        }
        #endregion
        #endregion

    }
    #region 控件内容封装
    #region 按钮类
    internal class ButtonContent
    {
        private string buttonContent_Img;
        /// <summary>
        /// 按钮图标
        /// </summary>
        public string ButtonContent_Img
        {
            get { return buttonContent_Img; }
            set { buttonContent_Img = value; }
        }

        private string buttonContent_Text;
        /// <summary>
        /// 按钮文字
        /// </summary>
        public string ButtonContent_Text
        {
            get { return buttonContent_Text; }
            set { buttonContent_Text = value; }
        }

        private string funcKey;

        public string FuncKey
        {
            get { return funcKey; }
            set { funcKey = value; }
        }

    }
    #endregion
    #endregion
    #region 转换器
    /// <summary>
    /// string转ImageSource
    /// </summary>
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
                    return new BitmapImage(new Uri((@"C:\我的资源\Resource\Image\Singer\" + value.ToString() + ".png"), UriKind.Relative));
                string str = AppDomain.CurrentDomain.BaseDirectory + @"Image\Singer\" + value.ToString() + ".png";
                return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    ///// <summary>
    ///// string转ImageBrush
    ///// </summary>
    //public class IconSourceConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is string)
    //        {
    //            System.Windows.Media.ImageBrush ib = new System.Windows.Media.ImageBrush();
    //            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
    //            {
    //                ib.ImageSource = new BitmapImage(new Uri(@"C:\我的资源\Resource\Image\Icon\" + value.ToString() + ".png", UriKind.Absolute));
    //                return ib;
    //            }
    //            string str = AppDomain.CurrentDomain.BaseDirectory + @"Image\Icon\" + value.ToString() + ".png";
    //            ib.ImageSource = new BitmapImage(new Uri(str, UriKind.Absolute));
    //            return ib;
    //        }
    //        return null;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return null;
    //    }
    //}
    #endregion
}
