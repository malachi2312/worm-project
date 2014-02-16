using GameFrameWork.Core;
using GameFrameWork.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CAnimator
    {
        protected Dictionary<string,CAnimation> pListAnimation;
        protected CAnimation currentActiveAnimation;
        protected string currentActiveAnimationName;
        public int numAnimation { get; set; }

        public CAnimator()
        {
            pListAnimation = new Dictionary<string, CAnimation>();
            numAnimation = 0;
            currentActiveAnimation = null;
            currentActiveAnimationName = "";
        }

        public bool addAnimation( string animtionName , CAnimation pAnim)
        {
            if (!pListAnimation.ContainsKey(animtionName))
            {
                pListAnimation.Add(animtionName, pAnim);
                return true;
            }

            return false;
        }

        public CAnimation play(string animationName)
        {
            if (!pListAnimation.ContainsKey(animationName))
            {
                CLog.create("Cannot find animtion");
                return null;
            }

            stop();
            currentActiveAnimation = pListAnimation[animationName];
            currentActiveAnimationName = animationName;

            currentActiveAnimation.start();

            return currentActiveAnimation;
        }

        public CAnimation stop()
        {
            if (currentActiveAnimation != null)
            {
                currentActiveAnimationName = "";
                currentActiveAnimation.stop();
                return currentActiveAnimation;
            }

            return null;
        }

        public CAnimation stop(string animatonName)
        {
            if (pListAnimation.ContainsKey(animatonName))
            {
                if (currentActiveAnimation == pListAnimation[animatonName])
                {
                    return stop();
                }
            }

            return null;
        }

        public CAnimation getCurrAnimation()
        {
            return currentActiveAnimation;
        }

        public CAnimation getAnimation(string animationName)
        {
            if (pListAnimation.ContainsKey(animationName))
            {
                return pListAnimation[animationName];
            }

            return null;
        }

        public string getCurrAnimationName()
        {
            return currentActiveAnimationName;
        }
    }
}
