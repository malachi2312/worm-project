using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Utilities
{
    class CLog
    {
        public static void create( string value )
        {
            Debugger.Log(1, "LOG", value + "\n" );
        }
    }
}
