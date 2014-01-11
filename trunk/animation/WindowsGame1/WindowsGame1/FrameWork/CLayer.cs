using GameFrameWork.Core;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Base
{
    class CLayer : CNode
    {
        protected int touchPriority;
        protected bool isTouchEnable;
        protected bool isMultiTouchEnable;
        protected bool isGestureEnable;


        public CLayer()
        {
            touchPriority = 0;
            isTouchEnable = false;
            isMultiTouchEnable = false;
            isGestureEnable = false;
        }

        public override bool init()
        {
            bool bRet = false;
            do
            {
                if (CDirector.sharedDirector() == null)
                {
                    CLog.create("Failed to get director/nCannot create new scene");
                    break;
                }

                base.init();

                this.setContentSize(CDirector.sharedDirector().getVisibleSize());
                this.ignoreAnchorPointForPosition(true);

                isTouchEnable = false;
                isMultiTouchEnable = false;
                isGestureEnable = false;

                bRet = true;
            } while (false);

            return bRet;
        }

        public override void destroy()
        {
            //if (this.isTouchEnable)
            //{
            //    unregisterTouchDispatcher();
            //}

            //if (this.isMultiTouchEnable)
            //{
            //    unregisterMultiTouchWithTouchDispatcher();
            //}

            //if (this.isGestureEnable)
            //{
            //    unregisterGestureWithTouchDispatcher();
            //}
            base.destroy();
        }

        //public virtual void registerWithTouchDispatcher()
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().registerTouchEvent(this, this.touchPriority);
        //}

        //public virtual void unregisterTouchDispatcher()
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().unregisterTouchEvent(this);
        //}

        //public virtual void registerMultiTouchWithTouchDispatcher()
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().registerMultiTouchEvent(this, this.touchPriority);
        //}

        //public virtual void unregisterMultiTouchWithTouchDispatcher()
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().unregisterMultiTouchEvent(this);
        //}

        //public virtual void registerGestureWithTouchDispatcher(bool isClaimed)
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().registerGestureEvent(this, this.touchPriority, isClaimed);
        //}

        //public virtual void registerGestureWithTouchDispatcher()
        //{
        //    registerGestureWithTouchDispatcher(false);
        //}

        //public virtual void unregisterGestureWithTouchDispatcher()
        //{
        //    CDirector.sharedDirector().getTouchDispatcher().unregisterGestureEvent(this);
        //}


        //public void setTouchPriority(int priority)
        //{
        //    if (this.touchPriority != priority)
        //    {
        //        this.touchPriority = priority;

        //        if (isTouchEnable)
        //        {
        //            this.setTouchEnable(false);
        //            this.setTouchEnable(true);
        //        }
        //    }
        //}

        //public void setTouchEnable(bool isTouchEnable)
        //{
        //    if (this.isTouchEnable == isTouchEnable)
        //        return;

        //    if (this.isMultiTouchEnable)
        //    {
        //        CLog.create("Only one touch mode at same time");
        //        this.setMultiTouchEnable(false);
        //    }

        //    if (isTouchEnable)
        //    {
        //        registerWithTouchDispatcher();
        //    }
        //    else
        //    {
        //        unregisterTouchDispatcher();
        //    }
        //    this.isTouchEnable = isTouchEnable;
        //}

        //public bool getIsTouchEnable()
        //{
        //    return this.isTouchEnable;
        //}


        //public void setMultiTouchEnable(bool isMultiTouch)
        //{
        //    if (this.isMultiTouchEnable = isMultiTouch)
        //        return;

        //    if (this.isTouchEnable)
        //    {
        //        CLog.create("Only one touch mode at same time");
        //        this.setTouchEnable(false);
        //    }

        //    if (isMultiTouch)
        //    {
        //        registerMultiTouchWithTouchDispatcher();
        //    }
        //    else
        //    {
        //        unregisterMultiTouchWithTouchDispatcher();
        //    }

        //    this.isMultiTouchEnable = isMultiTouch;
        //}

        //public bool getIsMultiTouchEnable()
        //{
        //    return this.isMultiTouchEnable;
        //}

        //public void setGestureEnable(bool isGesture)
        //{
        //    setGestureEnable(isGesture, false);
        //}

        //public void setGestureEnable(bool isGesture , bool isClaimed)
        //{
        //    if (this.isGestureEnable != isGesture)
        //    {
        //        if (isGesture)
        //        {
        //            registerGestureWithTouchDispatcher(isClaimed);
        //        }
        //        else
        //        {
        //            unregisterGestureWithTouchDispatcher();
        //        }

        //        this.isGestureEnable = isGesture;
        //    }
        //}


        //public virtual bool processTouch(TouchLocation pTouch)
        //{
        //    //CLog.create(pTouch.State.ToString());

        //    return false;
        //}

        //public virtual bool processMultitouch(TouchCollection pTouches)
        //{
        //    return false;   
        //}

        //public virtual bool onTap(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onDoubleTap(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onFlick(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onFreeDrag(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onHold(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onHorizontalDrag(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onVerticalDrag(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onPinch(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onPinchComplete(GestureSample gesture)
        //{
        //    return false; 
        //}

        //public virtual bool onDragComplete(GestureSample gesture)
        //{
        //    return false; 
        //}
    }

    class CLayerColor : CLayer
    {
        public static CLayerColor create(byte r, byte g, byte b, byte a)
        {
            CLayerColor layer = new CLayerColor();
            layer.initWithColor(r, g, b, a);

            return layer;
        }

        protected Color color;
        protected CTexture layerColor;
        protected CSize savedContentSize;

        public CLayerColor()
        {
            color = Color.White;
            layerColor = new CTexture();
            savedContentSize = CSize.SizeZero;
        }

        public override bool init()
        {
            return base.init();
        }

        public bool initWithColor(byte r, byte g, byte b, byte oppacity)
        {
            if (!this.init())
                return false;

            color = new Color(r, g, b);

            this.setOppacity(oppacity);

            return true;
        }

        protected void updateColor()
        {
            if (!CSize.isEqual(savedContentSize, this.getContentSize()))
            {
                GraphicsDevice device = CDirector.sharedDirector().getSharedSpriteBatch().GraphicsDevice;
                int i, j;
                float width = this.getContentSize().width;
                float height = this.getContentSize().height;
                Texture2D pTempColor = new Texture2D(device, (int)width, (int)height);
                Color[] listColor = new Color[(int)width * (int)height];

                for (i = 0; i < height; ++i)
                {
                    for (j = 0; j < width; ++j)
                    {
                        listColor[i * (int)width + j] = Color.White;
                    }
                }

                pTempColor.SetData<Color>(listColor);

                layerColor.setImage(pTempColor);
                layerColor.setBatchNode(CDirector.sharedDirector().getSharedSpriteBatch());
                savedContentSize = this.getContentSize();
            }
        }

        public override void draw()
        {
            updateColor();

            if( layerColor != null ){
                layerColor.draw(
                    this.pRelativeTransformation.position,
                    null, color, alpha,
                    this.pRelativeTransformation.rotation,
                    this.pRelativeTransformation.scale, SpriteEffects.None);
            }
        }
    }
}
