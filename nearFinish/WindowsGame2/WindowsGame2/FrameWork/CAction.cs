using GameFrameWork.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.System
{
    class CAction : CObject
    {
        protected int tag;
        protected CNode pTarget;
        public bool isRunning { set; get; }

        public CAction()
        {
            tag = 0;
            pTarget = null;
            isRunning = false;
        }

        public void setTarget(CNode target)
        {
            this.pTarget = target;
        }

        public CNode getTarget()
        {
            return this.pTarget;
        }

        virtual public void startWithTarget(CNode pTarget)
        {

        }

        virtual public bool isDone( )
        {
            return !this.isRunning;
        }

        virtual public void stop()
        {
            this.pTarget = null;
        }

        virtual public void setTag(int tag)
        {
            this.tag = tag;
        }

        virtual public int getTag()
        {
            return this.tag;
        }

        virtual public void update(float dt)
        {

        }
    }
}
