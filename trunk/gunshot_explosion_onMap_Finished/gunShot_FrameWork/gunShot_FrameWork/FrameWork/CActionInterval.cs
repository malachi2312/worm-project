using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GameFrameWork.Core.System
{
    class CActionInterval : CFiniteTimeAction
    {
        protected float timeElapse;

        public CActionInterval()
        {
            timeElapse = 0.0f;
        }

        public void initWithDuration(float d)
        {
            this.duration = d;

            if (this.duration == 0)
            {
                this.duration = float.MinValue;   
            }

            this.timeElapse = 0.0f;
        }
    }

    class CRotateTo : CActionInterval
    {
        protected float destAngle;
        protected float angleToRotatePerSec;

        public static CRotateTo create( float duration , float destAngle )
        {
            CRotateTo pAct = new CRotateTo();
            pAct.init( duration , destAngle );

            return pAct;
        }

        public CRotateTo()
        {
            destAngle = 0;
            angleToRotatePerSec = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);

            float startAngle = pTarget.getRotation();

            this.angleToRotatePerSec = (destAngle - startAngle) / duration;

            this.isRunning = true;

            base.startWithTarget(pTarget);
        }

        public void init(float duration, float destAngle)
        {
            this.initWithDuration(duration);
            this.destAngle = destAngle;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                pTarget.setRotation( pTarget.getRotation() + this.angleToRotatePerSec * dt);

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                    pTarget.setRotation(destAngle);
                }
            }
        }
    }

    class CRotateBy : CRotateTo
    {
        protected float addedAngle;

        public new static CRotateBy create(float duration, float addedAngle)
        {
            CRotateBy pAct = new CRotateBy();
            pAct.init(duration, addedAngle);

            return pAct;
        }

        public CRotateBy()
        {
            destAngle = 0;
            addedAngle = 0;
            angleToRotatePerSec = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            float startAngle = pTarget.getRotation();

            this.destAngle = startAngle + addedAngle;

            base.startWithTarget(pTarget);
        }

        public new void init(float duration, float addedAngle)
        {
            this.initWithDuration(duration);
            this.addedAngle = addedAngle;
        }

        public override void update(float dt)
        {
            base.update(dt);
        }
    }

    class CMoveTo : CActionInterval
    {
        protected CPoint startPoint;
        protected CPoint destPoint;
        protected float angleToTarget;
        protected float speedPerSec;
        protected CPoint addedVelocity;

        public static CMoveTo create(float duration, CPoint dest)
        {
            CMoveTo pAct = new CMoveTo();
            pAct.init(duration, dest);

            return pAct;
        }

        public CMoveTo()
        {
            startPoint = CPoint.PointZero;
            destPoint = CPoint.PointZero;
            angleToTarget = 0;
            speedPerSec = 0;
            addedVelocity = CPoint.PointZero;
        }

        public override void startWithTarget( CNode pTarget)
        {
            this.setTarget(pTarget);

            this.startPoint = pTarget.getPosition() ;

            CPoint destPos = this.destPoint;
            
            //Convert to nodespcae 
            if (pTarget.getParent() != null)
            {
                destPos = pTarget.getParent().convertToLocalSpace(this.destPoint);
            }

            float deltaX = this.destPoint.x - this.startPoint.x;
            float deltaY = this.destPoint.y - this.startPoint.y;

            this.angleToTarget = (float)Math.Atan2(deltaY, deltaX);
            this.angleToTarget = 90.0f + (this.angleToTarget * (180.0f / (float)Math.PI));

            float distance = CPoint.Distance(this.startPoint, destPos);

            this.speedPerSec = distance / this.duration;

            float vX = (float)Utilities.Utilities.LinearVelocityX(this.angleToTarget) * speedPerSec;
            float vY = (float)Utilities.Utilities.LinearVelocityY(this.angleToTarget) * speedPerSec;

            this.addedVelocity = CPoint.create( vX , vY );

            this.isRunning = true;
        }

        public void init(float duration, CPoint destPoint)
        {
            this.initWithDuration(duration);
            this.destPoint = destPoint;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                pTarget.setPosition( pTarget.getPosition().x + addedVelocity.x * dt , pTarget.getPosition().y + addedVelocity.y * dt);

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                    pTarget.setPosition(destPoint);
                }
            }
        }
    }

    class CMoveBy : CMoveTo
    {
        protected CPoint distancePoint;

        public new static CMoveBy create(float duration, CPoint addedVel)
        {
            CMoveBy pAct = new CMoveBy();
            pAct.init(duration, addedVel);

            return pAct;
        }

        public CMoveBy()
        {
            
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.destPoint.x = pTarget.getPosition().x + distancePoint.x;
            this.destPoint.y = pTarget.getPosition().y + distancePoint.y;

            base.startWithTarget(pTarget);
        }


        public new void init(float duration, CPoint distancePoint)
        {
            this.initWithDuration(duration);
            this.distancePoint = distancePoint;
        }

        public override void update(float dt)
        {
            base.update(dt);
        }
    }

    class CScaleTo : CActionInterval
    {
        protected float endScaleX;
        protected float endScaleY;
        protected float scaleXPerSec;
        protected float scaleYPerSec;

        public static CScaleTo create(float duration , float scaleTo)
        {
            CScaleTo pAct = new CScaleTo();
            pAct.init(duration, scaleTo);

            return pAct;
        }

        public CScaleTo()
        {
            endScaleX = 0;
            endScaleY = 0;
            scaleXPerSec = 0;
            scaleYPerSec = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);

            float startScaleX = pTarget.getScaleX();
            float startScaleY = pTarget.getScaleY();

            scaleXPerSec = (endScaleX - startScaleX) / duration;
            scaleYPerSec = (endScaleY - startScaleY) / duration;

            this.isRunning = true;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                pTarget.setScaleX(pTarget.getScaleX() + scaleXPerSec * dt);
                pTarget.setScaleY(pTarget.getScaleY() + scaleYPerSec * dt);

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                    pTarget.setScaleX(endScaleX);
                    pTarget.setScaleY(endScaleY);
                }
            }
        }

        public void init(float duration, float scaleTo)
        {
            this.initWithDuration(duration);
            this.endScaleX = scaleTo;
            this.endScaleY = scaleTo;
        }
    }

    class CScaleBy : CScaleTo
    {
        protected float scaleBy;

        public new static CScaleBy create(float duration, float scaleTo)
        {
            CScaleBy pAct = new CScaleBy();
            pAct.init(duration, scaleTo);

            return pAct;
        }

        public CScaleBy()
        {
            endScaleX = 0;
            endScaleY = 0;
            scaleXPerSec = 0;
            scaleYPerSec = 0;
            scaleBy = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            float startScaleX = pTarget.getScaleX();
            float startScaleY = pTarget.getScaleY();

            endScaleX = startScaleX + scaleBy;
            endScaleY = startScaleY + scaleBy;

            base.startWithTarget(pTarget);
        }

        public override void update(float dt)
        {
            base.update(dt);
        }

        public new void init(float duration, float scaleBy)
        {
            this.initWithDuration(duration);
            this.scaleBy = scaleBy;
        }
    }

    class CFadeIn : CActionInterval
    {
        protected float fadePerSec;
        protected float fadeRatio;

        public static CFadeIn create(float duration)
        {
            CFadeIn pAct = new CFadeIn();
            pAct.init(duration);

            return pAct;
        }

        public CFadeIn()
        {
            fadePerSec = 0;
            fadeRatio = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);

            float startOpp = 0;
            float endOpp = 255;

            if (pTarget.getOppacity() < 255)
            {
                startOpp = pTarget.getOppacity();
            }

            fadeRatio = startOpp;

            fadePerSec = ( endOpp - startOpp ) / this.duration;

            this.isRunning = true;
        }

        public void init(float duration)
        {
            this.initWithDuration(duration);
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                fadeRatio += fadePerSec * dt;

                pTarget.setOppacity( fadeRatio );

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                    pTarget.setOppacity(255);
                }
            }
        }
    }

    class CFadeOut : CActionInterval
    {
        protected float fadePerSec;
        protected float fadeRatio;

        public static CFadeOut create(float duration)
        {
            CFadeOut pAct = new CFadeOut();
            pAct.init(duration);

            return pAct;
        }

        public CFadeOut()
        {
            fadePerSec = 0;
            fadeRatio = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);

            float startOpp = 255;
            float endOpp = 0;

            if (pTarget.getOppacity() > 0)
            {
                startOpp = pTarget.getOppacity();
            }

            fadeRatio = startOpp;

            fadePerSec = ( endOpp - startOpp ) / this.duration;
            this.isRunning = true;
        }

        public void init(float duration)
        {
            this.initWithDuration(duration);
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                fadeRatio += fadePerSec * dt;

                pTarget.setOppacity( fadeRatio);

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                    pTarget.setOppacity(0);
                }
            }
        }
    }

    class CExtraAction : CActionInterval
    {
        public static CExtraAction create()
        {
            CExtraAction pAct = new CExtraAction();

            return pAct;
        }

        public CExtraAction()
        {

        }
    }

    class CSequence : CActionInterval
    {
        protected List<CFiniteTimeAction> pListActions;
        protected CFiniteTimeAction[] pActions;

        public static CSequence create(params CFiniteTimeAction[] pListAct)
        {
            CSequence pAct = new CSequence();
            pAct.init(pListAct);

            return pAct;
        }

        public static CSequence createWithTwoActions(CFiniteTimeAction pAct_1, CFiniteTimeAction pAct_2)
        {
            CSequence pAct = new CSequence();
            pAct.initWithTwoActions(pAct_1, pAct_2);

            return pAct;
        }

        public CSequence()
        {
            pListActions = new List<CFiniteTimeAction>();
            pActions = new CFiniteTimeAction[2];
        }

        public override void startWithTarget(CNode pTarget)
        {
            this.setTarget(pTarget);

            pActions[0].startWithTarget(pTarget);
            pActions[1].setTarget(null);

            this.isRunning = true;
        }

        public void initWithTwoActions(CFiniteTimeAction pAct_1, CFiniteTimeAction pAct_2)
        {
            this.initWithDuration(pAct_1.getDuration() + pAct_2.getDuration() );

            pActions[0] = pAct_1;
            pActions[1] = pAct_2;

            this.isRunning = true;
        }

        public void init( params CFiniteTimeAction[] pListAct )
        {
            pListActions = pListAct.ToList<CFiniteTimeAction>();

            int listCount = pListActions.Count;

            if (listCount <= 0)
            {
                this.isRunning = false;
                return;
            }

            if (listCount > 1)
            {
                CFiniteTimeAction pAct_1 = pListActions[1];

                for (int i = 2; i < listCount; ++i)
                {
                    pAct_1 = CSequence.createWithTwoActions(pAct_1, pListActions[i]);
                }

                this.initWithTwoActions(pListActions[0], pAct_1);
            }
            else
            {
                this.initWithTwoActions(pListActions[0], CExtraAction.create());
            }
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                if (!pActions[0].isDone())
                {
                    //Trace.WriteLine("Pre start action 1");
                    if (pActions[0].getTarget() == null)
                    {
                        pActions[0].startWithTarget(this.getTarget());
                        
                    }
                    pActions[0].update(dt);
                }
                else
                {
                    //Trace.WriteLine("Pre start action 2");
                    if (pActions[1].getTarget() == null)
                    {
                        pActions[1].startWithTarget(this.getTarget());
                    }
                    pActions[1].update(dt);
                }

                if (this.timeElapse >= this.duration && pActions[1].isDone())
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                }
            }
        }
    }

    class CDelayTime : CActionInterval
    {

        public static CDelayTime create(float duration)
        {
            CDelayTime pAct = new CDelayTime();
            pAct.initWithDuration(duration);  

            return pAct;
        }

        public CDelayTime()
        {

        }

        public override void startWithTarget(CNode pTarget)
        {
            base.startWithTarget(pTarget);
            this.isRunning = true;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                this.timeElapse += dt;

                if (this.timeElapse >= this.duration)
                {
                    this.timeElapse = 0;
                    this.isRunning = false;
                }
            }
            base.update(dt);
        }
    }

    class CRepeat : CActionInterval
    {
        public static CRepeat create(CFiniteTimeAction pInnerAction, int counts)
        {
            CRepeat pAct = new CRepeat();
            pAct.init(pInnerAction, counts);

            return pAct;
        }

        protected int localCount;
        protected int maxCount;
        protected CFiniteTimeAction pInnerAction;

        public CRepeat()
        {
            maxCount = 0;
            localCount = 0;
        }

        public override void startWithTarget(CNode pTarget)
        {
            pInnerAction.startWithTarget(pTarget);
            this.setTarget(pTarget);
            isRunning = true;
        }

        public void init( CFiniteTimeAction pActionInterval ,  int counts)
        {
            pInnerAction = pActionInterval;
            localCount = 0;
            maxCount = counts;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                pInnerAction.update(dt);

                if (!pInnerAction.isRunning)
                {
                    localCount += 1;
                    if (localCount < maxCount)
                    {
                        pInnerAction.startWithTarget(pTarget);
                        pInnerAction.isRunning = true;
                    }
                    else
                    {
                        this.isRunning = false;
                    }
                }
            }
        }
    }

    class CRepeatForever : CActionInterval
    {
        public static CRepeatForever create(CFiniteTimeAction pInnerAction)
        {
            CRepeatForever pAct = new CRepeatForever();
            pAct.init(pInnerAction);

            return pAct;
        }

        protected CFiniteTimeAction pInnerAction;

        public CRepeatForever()
        {
            pInnerAction = null;
        }

        public override void startWithTarget(CNode pTarget)
        {
            pInnerAction.startWithTarget(pTarget);
            this.setTarget(pTarget);
            isRunning = true;
        }

        public void init(CFiniteTimeAction pAction)
        {
            pInnerAction = pAction;
        }

        public override void update(float dt)
        {
            if (this.isRunning)
            {
                pInnerAction.update(dt);

                if (pInnerAction.isDone())
                {
                    pInnerAction.startWithTarget(this.getTarget());
                }
            }
        }
    }

}
