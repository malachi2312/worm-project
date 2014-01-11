using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;


namespace WindowsGame1
{
    class MenuScene : CLayer
    {

        Player player;
        public static CScene scene()
        {
            CScene scene = new CScene();
            MenuScene layer = new MenuScene();

            scene.addChild(layer);
            return scene;
        }

        private CNode mNode;
        public MenuScene()
        {

            player = new Player();
            this.addChild(player);
            System.Diagnostics.Trace.WriteLine("create player");

            CSize winSize = CDirector.sharedDirector().getVisibleSize();
            mNode = new CNode();
            mNode.setPosition(200, 240);
            this.addChild(mNode);

            //CRotateBy rotateBy = CRotateBy.create(5, 360);
            //CRepeatForever repeatForever = CRepeatForever.create(rotateBy);
            //mNode.runAction(repeatForever);

            CMoveTo moveTo = CMoveTo.create(2, CPoint.create(600.0f, 240.0f));
            CMoveTo moveTo2 = CMoveTo.create(2, CPoint.create(200.0f, 240.0f));
            CSequence seq = CSequence.create(moveTo, moveTo2);
            CRepeatForever repeatForever2 = CRepeatForever.create(seq);
            mNode.runAction(repeatForever2);

            for (int i = 0; i < 6; i++)
            {
                CSprite sprite = CSprite.create("heart");
                sprite.setAnchorPoint(CPoint.create(0.5f, -2f));
                sprite.setRotation(i * 60);
                mNode.addChild(sprite);

                CFadeOut fadeOut = CFadeOut.create(2);
                CFadeIn fadeIn = CFadeIn.create(2);
                CSequence sequence = CSequence.create(fadeOut, fadeIn);
                CRepeatForever repeatF = CRepeatForever.create(sequence);
                sprite.runAction(repeatF);
            }

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
                    mNode.stopAllAction();
                    foreach (CNode child in mNode.getChildren())
                    {
                        child.stopAllAction();
                    }
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(AboutScene.scene());
                }
            }

           
            //Trace.WriteLine("update");
            //mNode.setPosition(mNode.getPosition().x, mNode.getPosition().y + 30 * dt);
        }
    }

}