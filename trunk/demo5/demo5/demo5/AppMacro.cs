using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Utilities
{
    class AppMacro
    {

        public enum PLATFORM {
            kPlatformWindows = 1,
            kPlatformWindowsPhone = 2,
            kPlatformXbox = 3
        }

        public static PLATFORM TARGET_PLATFORM = PLATFORM.kPlatformWindowsPhone;

        public static DisplayOrientation ORIENTATION = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

        public static float WINSIZE_WIDTH = 800;
        public static float WINSIZE_HEIGHT = 480;
    }
}
