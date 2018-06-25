using AppModel.FuncBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMCMusic
{
    [AppModel.UMCAttributes.UMC(AppModel.UMCAttributes.FuncType.Func, typeof(MusicProcess), AppModel.Common.FuncCode.Music)]
    public class MusicProcess:FuncBase
    {
        public MusicProcess()
        {
            Name = "发现音乐";
            Key = "2";
            Type = typeof(Music);
        }
        public override int Start()
        {
            return 0;
        }
    }
}
