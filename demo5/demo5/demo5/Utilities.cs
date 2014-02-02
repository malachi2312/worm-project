using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Utilities
{
    public enum LayerOrder
    {
        kLayerOrderLowest = 0,
        kLayerOrderMiddle = 50,
        kLayerOrderTop = 1000,
        kLayerOrderTopMiddle = 5000,
        kLayerOrderTopMost = 9999
    };

    class Utilities
    {
        private static Utilities s_Utilities = null;

        private Random m_pRandom;

        private Utilities()
        {
            DateTime time = DateTime.Now;
            m_pRandom = new Random(time.Millisecond);
        }


        public static Utilities sharedUtilities()
        {
            if (s_Utilities == null)
            {
                s_Utilities = new Utilities();
            }

            return s_Utilities;
        }


        public static double LinearVelocityX(float angle)
        {
            angle -= 90;
            if (angle <= 0)
                angle = 360 + angle;

            return Math.Cos( (double)(angle * Math.PI / 180));
        }

        public static double LinearVelocityY(float angle)
        {
            angle -= 90;
            if (angle <= 0)
                angle = 360 + angle;

            return Math.Sin((double)(angle * Math.PI / 180));
        }

        public static float radianToDegrees(float radian)
        {
            return radian * 180.0f / (float)Math.PI;
        }

        public static float degreesToRadian(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public int randomNumber(int min, int max)
        {
            return m_pRandom.Next(min, max);
        }
    }
}
