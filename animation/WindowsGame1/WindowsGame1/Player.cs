using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
namespace WindowsGame1
{
    class Player: CNode
    {
       public  CAnimationSprite aSprite = CAnimationSprite.create("sprites", 144, 144);
        CAnimation run = CAnimation.create(144, 144, 6, 12, 17, false, 0.2f);
        CAnimation idle = CAnimation.create(144, 144, 5, 0, 4, false, 0.2f);

        public Player()
        {
            //animation.isSequence = true;
                aSprite.getAnimator().addAnimation("run", run);
                aSprite.getAnimator().addAnimation("idle", idle);
              

                this.addChild(aSprite);                  
    
                aSprite.setPosition(CPoint.create(200, 200));
            System.Diagnostics.Trace.WriteLine("create player");
            this.scheduleUpdate();
        }

        //bool mIsKeyADown;
        public override void update(float dt)
        {
           // bool lastState = mIsKeyADown;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (!run.isRunning)
                {
                    aSprite.getAnimator().play("run");
                }
                //mIsKeyADown = true;
                //System.Diagnostics.Trace.WriteLine("create player");
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.A))
            {
                if(!idle.isRunning)
                aSprite.getAnimator().play("idle");
            }
               
            //else if (Keyboard.GetState().IsKeyUp(Keys.A))
            //{
            //    mIsKeyADown = false;
            //}

            //if (lastState == false && mIsKeyADown == true)
            //{
            //    aSprite.getAnimator().play("run");
            //}
        }

      
           
               
       
    }
}
