using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.System
{
    class CActionInstant : CFiniteTimeAction
    {
        public CActionInstant()
        {

        }
    }


    class CCallbackFunc : CActionInstant
    {
        public static CCallbackFunc create(callback_func method)
        {
            CCallbackFunc pAct = new CCallbackFunc();
            pAct.init(method);

            return pAct;
        }

        public delegate void callback_func();

        protected callback_func targetFunc;

        public CCallbackFunc()
        {
            this.targetFunc = null;
        }

        public void init(callback_func func)
        {
            this.targetFunc = func;
            this.isRunning = true;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.targetFunc.Invoke();
                this.isRunning = false;
            }
        }
    }

    class CCallbackFuncN : CActionInstant
    {
        public static CCallbackFuncN create(callback_funcN method)
        {
            CCallbackFuncN pAct = new CCallbackFuncN();
            pAct.init(method);

            return pAct;
        }

        public delegate void callback_funcN(CNode pObj);

        protected callback_funcN targetFunc;

        public CCallbackFuncN()
        {
            this.targetFunc = null;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);
        }

        public void init(callback_funcN func)
        {
            this.targetFunc = func;
            this.isRunning = true;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.targetFunc.Invoke(this.getTarget());
                this.isRunning = false;
            }
        }
    }

    class CCallbackFuncND : CActionInstant
    {
        public static CCallbackFuncND create(callback_funcND method , object data)
        {
            CCallbackFuncND pAct = new CCallbackFuncND();
            pAct.init(method , data);

            return pAct;
        }

        public delegate void callback_funcND(CNode pObj , object data);

        protected callback_funcND targetFunc;
        protected object data;

        public CCallbackFuncND()
        {
            this.targetFunc = null;
            this.data = null;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);
        }

        public void init(callback_funcND func , object data)
        {
            this.targetFunc = func;
            this.data = data;
            this.isRunning = true;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.targetFunc.Invoke(this.getTarget() , data);
                this.isRunning = false;
            }
        }
    }

    class CPlace : CActionInstant
    {
        public static CPlace create(CPoint pos)
        {
            CPlace pAct = new CPlace();
            pAct.init(pos);

            return pAct;
        }

        protected CPoint position;

        public CPlace()
        {

        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);
            isRunning = true;
        }

        public void init(CPoint pos)
        {
            position = pos;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                pTarget.setPosition(position);
                isRunning = false;
            }
        }
    }
}
