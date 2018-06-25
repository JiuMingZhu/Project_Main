using AppModel.Common;
using AppModel.FuncBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UMCSearch
{
    [AppModel.UMCAttributes.UMC(AppModel.UMCAttributes.FuncType.Func, typeof(SearchProcess), AppModel.Common.FuncCode.Search)]
    public class SearchProcess:FuncBase
    {
        public SearchProcess()
        {
            Name = "搜索";
            Key = "1";
            Type = typeof(Search);
        }
        public override int Start()
        {
            //to do.
            return 0;
        }
    }
}
