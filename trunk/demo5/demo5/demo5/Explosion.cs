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
namespace demo5
{
    class Explosion: CNode
    {
         public CAnimationSprite explosionSprite = CAnimationSprite.create("bomb", 278, 235);
        CAnimation run = CAnimation.create(278, 235, 3, 0, 9, false, 0.1f);
        
        public Color[,] explosionColorArray;
        public Explosion(Terrain map)
        {
            explosionSprite.setScale(0.2f);
            explosionSprite.getAnimator().addAnimation("run", run);
            this.addChild(explosionSprite);
            explosionColorArray = map.TextureTo2DArray(explosionSprite.getTexture());

           
        }

        public void explode()
        {
            explosionSprite.getAnimator().play("run");
            this.scheduleUpdate();
        }

        public override void update(float dt)
        {
            if (explosionSprite.getAnimator().getAnimation("run").isAnimationEnd())
            {
                this.unscheduleUpdate();
                this.removeFromParentWithCleanUp();
            }
        }

       
           
    }
    
}
