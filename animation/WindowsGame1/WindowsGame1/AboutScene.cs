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
    class AboutScene : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            AboutScene layer = new AboutScene();

            scene.addChild(layer);
            return scene;
        }

        CSprite sprite;
        public AboutScene()
        {
            sprite = CSprite.create("heart");
            sprite.setPosition(200, 100);
            this.addChild(sprite);

            CSprite sprite2 = CSprite.create("heart");
            sprite2.setAnchorPoint(CPoint.create(0, 0));
            sprite2.setPosition(140, 0);
            sprite.addChild(sprite2);

            CMoveTo moveTo1 = CMoveTo.create(2, CPoint.create(400, 100));
            CDelayTime delay1 = CDelayTime.create(1);
            CMoveTo moveTo2 = CMoveTo.create(2, CPoint.create(600, 300));
            CDelayTime delay2 = CDelayTime.create(1);
            CMoveTo moveTo3 = CMoveTo.create(2, CPoint.create(200, 100));
            CSequence sequence = CSequence.create(moveTo1, delay1, moveTo2, delay2, moveTo3);
            CRepeatForever repeat = CRepeatForever.create(sequence);
            sprite.runAction(repeat);

            this.scheduleUpdate();
        }


        private ButtonState mLastState = ButtonState.Released;
        public override void update(float dt)
        {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                mLastState = ButtonState.Pressed;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                if (mLastState == ButtonState.Pressed)
                {
                    sprite.stopAllAction();
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(MenuScene.scene());
                }
            }
            //Trace.WriteLine("update");
            //mNode.setPosition(mNode.getPosition().x, mNode.getPosition().y + 30 * dt);
        }
    }
}
