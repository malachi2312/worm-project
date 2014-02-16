using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CSize : CComponent
    {

        public static CSize SizeZero
        {
            get
            {
                return new CSize();
            }
        }

        public float width { set; get; }
        public float height { set; get; }


        public CSize()
        {
            width = 0;
            height = 0;
        }

        public CSize(float _width, float _height)
        {
            width = _width;
            height = _height;
        }

        //Utilities methods
        //public static CSize SizeZero()
        //{
        //    return new CSize();
        //}

        public static CSize create(CSize s)
        {
            return CSize.create(s.width, s.height);
        }

        public static CSize create(float _width, float _height)
        {
            return new CSize(_width, _height);
        }

        public static bool isEqual(CSize s1, CSize s2)
        {
            return s1.width == s2.width
                && s1.height == s2.height;
        }
    }
}
