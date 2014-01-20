using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CPoint :  CComponent
    {
        public float x { set; get; }
        public float y { set; get; }
        public float z { set; get; }


        public static CPoint PointZero
        {
            get
            {
                return new CPoint() ;
            }
        }


        public CPoint()
        {
            x = y = z = 0;
        }

        public CPoint(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        //Utilities method
        
        ///// <summary>
        ///// Create Zero point
        ///// </summary>
        ///// <returns></returns>
        //public static CPoint PointZero()
        //{
        //    return new CPoint();
        //}

        /// <summary>
        /// Create Point with given value
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <returns></returns>
        public static CPoint create(float _x, float _y, float _z)
        {
            return new CPoint(_x, _y, _z);
        }

        public static CPoint create(float _x, float _y)
        {
            return new CPoint(_x, _y , 0);
        }

        public static CPoint create(CPoint p)
        {
            return CPoint.create(p.x, p.y, p.z);
        }

        public static float Distance(CPoint p1, CPoint p2)
        {
            return CPoint.Length(CPoint.sub(p1,p2));
        }

        public static float DistanceSq(CPoint p1, CPoint p2)
        {
            return CPoint.LengthSQ(CPoint.sub(p1, p2));
        }

        public static float Length(CPoint v)
        {
            return (float)Math.Sqrt((double)CPoint.LengthSQ(v));
        }

        public static float LengthSQ(CPoint v)
        {
            return CPoint.dot(v, v);
        }

        public static float dot(CPoint p1, CPoint p2)
        {
            return p1.x * p2.x + p1.y * p2.y;
        }

        public static float toAngle(CPoint p)
        {
            return (float)Math.Atan2((double)p.y, (double)p.x);
        }

        public static CPoint sub(CPoint p1, CPoint p2)
        {
            return CPoint.create(p1.x - p2.x, p1.y - p2.y);
        }

        /// <summary>
        /// This methods only compare 2 point in 2-dimensions
        /// That mean we only compare x and y value
        /// </summary>
        /// <param name="p1">The first point</param>
        /// <param name="p2">The secon point</param>
        /// <returns>True if equal
        ///             false if not</returns>
        public static bool isEqualIn2D(CPoint p1, CPoint p2)
        {
            return (p1.x == p2.x)
                && (p1.y == p2.y);
        }

        /// <summary>
        /// This methods compare full 3 point 
        /// We can use in 3-dimensions or when we want to compare all 3 value of 2 point
        /// </summary>
        /// <param name="p1">The first point</param>
        /// <param name="p2">The second point</param>
        /// <returns>True if equal
        ///             false if not</returns>
        public static bool isEqualIn3D(CPoint p1, CPoint p2)
        {
            return (p1.x == p2.x)
                && (p1.y == p2.y)
                && (p1.z == p2.z);
        }
    }
}
