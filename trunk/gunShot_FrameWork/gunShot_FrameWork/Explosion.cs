using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework;

namespace gunShot_FrameWork
{
    class Explosion: CNode
    {
        public CAnimationSprite aSprite = CAnimationSprite.create("bomb", 278, 235);
        CAnimation run = CAnimation.create(278, 235, 3, 0, 9, false, 0.1f);

        public Explosion()
        {
            aSprite.setScale(0.2f);
            aSprite.getAnimator().addAnimation("run", run);
            this.addChild(aSprite);
        }

        public void explode()
        {
            aSprite.getAnimator().play("run");
            this.scheduleUpdate();
        }

        public override void update(float dt)
        {
            if (aSprite.getAnimator().getAnimation("run").isAnimationEnd())
            {
                this.unscheduleUpdate();
                this.removeFromParentWithCleanUp();
            }
        }
    }
}
