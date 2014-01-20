using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CRect : CComponent
    {


        public static CRect RectZero
        {
            get
            {
                return new CRect();
            }
        }

        public CSize size;
        public CPoint origin;

        public CRect()
        {
            this.size = CSize.SizeZero;
            this.origin = CPoint.PointZero;
        }


        public CRect(float x, float y, float width, float height)
        {
            this.size = CSize.create(width, height);
            this.origin = CPoint.create(x,y);
        }

        public float getMaxX()
        {
            return origin.x + size.width;
        }

        public float getMinX()
        {
            return origin.x;
        }


        public float getMaxY()
        {
            return origin.y + size.height;
        }

        public float getMinY()
        {
            return origin.y;
        }


        public bool isContainPoint(CPoint point)
        {
            bool bRet = false;

            if (point.x >= getMinX() && point.x <= getMaxX()
                && point.y >= getMinY() && point.y <= getMaxY())
            {
                bRet = true;
            }

            return bRet;
        }

        public bool intersectRect(CRect rect)
        {
            return !(   getMaxX() < rect.getMinX() ||
                        rect.getMaxX() < getMinX() ||
                        getMaxY() < rect.getMinY() ||
                        rect.getMaxY() < getMinY());
        }


        //Utilities methods
        //public static CRect RectZero()
        //{
        //    return new CRect();
        //}

        public static CRect create(CRect r)
        {
            return CRect.create(r.origin.x, r.origin.y, r.size.width, r.size.height);
        }

        public static CRect create(float x, float y, float width, float height)
        {
            return new CRect(x, y, width, height);
        }

        public static bool isEqual(CRect r1, CRect r2)
        {
            return CPoint.isEqualIn2D(r1.origin, r2.origin)
                && CSize.isEqual(r1.size, r2.size);
        }

        public static CRect intersection(CRect r1, CRect r2)
        {
            float x1 = Math.Max(r1.getMinX(), r2.getMinX());
            float x2 = Math.Min(r1.getMaxX(), r2.getMaxX());

            float y1 = Math.Max(r1.getMinY(), r2.getMinY());
            float y2 = Math.Min(r1.getMaxY(), r2.getMaxY());

            return CRect.create(x1, y1, x2 - x1, y2 - y1);
        }
        
    }
}
