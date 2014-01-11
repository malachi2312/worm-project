using GameFrameWork.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CAnimation : CComponent
    {
        public int startIdx { get; set; }
        public int endIdx { get; set; }
        public bool isLoop { get; set; }
        public float animationFps { get; set; }
        public int loopIdx { get; set; }
        public bool isReverse { get; set; }
        public bool isSequence { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public int numFrameInRow { get; set; }

        public bool isRunning { get; set; }

        protected Rectangle sourceRect;

        protected int currIdx;
        protected float localTime;


        public static CAnimation create(float width, float height, int numFrameInRow, int start, int end, bool isLoop, float animationFps, int loopIdx)
        {
            CAnimation pAnim = new CAnimation();
            pAnim.initAnimation(width, height, numFrameInRow, start, end, isLoop, animationFps , loopIdx);

            return pAnim;
        }

        public static CAnimation create(float width, float height, int numFrameInRow, int start, int end, bool isLoop, float animationFps)
        {
            return CAnimation.create(width, height, numFrameInRow, start, end, isLoop, animationFps, start);
        }

        public CAnimation()
        {
            currIdx = 0;
            startIdx = 0;
            endIdx = 0;
            isLoop = false;
            localTime = 0;
            animationFps = 0;
            isReverse = false;
            isRunning = false;
        }

        public void initAnimation(float width, float height, int numFrameInRow , int start, int end, bool isLoop, float animationFps)
        {
            initAnimation(width , height , numFrameInRow , start, end, isLoop, animationFps, start);
        }

        public void initAnimation(float width, float height, int numFrameInRow, int start, int end, bool isLoop, float animationFps, int loopIdx)
        {
            this.width = width;
            this.height = height;
            this.numFrameInRow = numFrameInRow;
            this.currIdx = this.startIdx = start;
            this.endIdx = end;
            this.isLoop = isLoop;
            this.animationFps = animationFps;
            this.loopIdx = loopIdx;

            this.updateSourceRect();
        }

        public void start()
        {
            resetAnimation();
            isRunning = true;
            CDirector.sharedDirector().getScheduler().scheduleWithTarget(update);
        }

        public void stop()
        {
            isRunning = false;
            CDirector.sharedDirector().getScheduler().unscheduleWithTarget(update);
        }

        public void update(float dt)
        {
            if (isRunning)
            {
                this.localTime += dt;
                if (this.localTime >= this.animationFps)
                {
                    this.localTime -= this.animationFps;
                    if (isSequence)
                    {
                        //TODO update with sequence mode
                    }
                    else
                    {
                        if (!isReverse)
                        {
                            nextFrame();
                        }
                        else
                        {
                            previuosFrame();
                        }
                    }
                    updateSourceRect();
                }
            }
            else
            {
                CDirector.sharedDirector().getScheduler().unscheduleWithTarget(update);
            }
        }

        public Rectangle getSourceRect()
        {
            return sourceRect;
        }


        private void nextFrame()
        {
            currIdx += 1;
            if (currIdx > endIdx)
            {
                if (isLoop)
                    currIdx = loopIdx;
                else
                {
                    currIdx = endIdx;
                    isRunning = false;
                }
            }
        }

        private void previuosFrame()
        {
            currIdx -= 1;
            if (currIdx < startIdx)
            {
                if (isLoop)
                    currIdx = endIdx;
                else
                {
                    currIdx = startIdx;
                    isRunning = false;
                }
            }
        }


        public void updateSourceRect()
        {
            sourceRect.X = (currIdx % numFrameInRow) * (int)width;
            sourceRect.Y = (currIdx / numFrameInRow) * (int)height;
            sourceRect.Width = (int)width;
            sourceRect.Height = (int)height;
        }

        public bool isAnimationEnd()
        {
            return !isRunning;
        }

        public void resetAnimation()
        {
            currIdx = startIdx;
        }
    }

}
