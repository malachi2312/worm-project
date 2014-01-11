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
        CAnimation animation = CAnimation.create(144, 144, 6, 12, 17, true, 0.2f);

        public Player()
        {
           
                aSprite.getAnimator().addAnimation("run", animation);
                aSprite.getAnimator().play("run");
                this.addChild(aSprite);
                
                    
    
                aSprite.setPosition(CPoint.create(200, 200));
            System.Diagnostics.Trace.WriteLine("create player");
        }

      
           
               
       
    }
}
