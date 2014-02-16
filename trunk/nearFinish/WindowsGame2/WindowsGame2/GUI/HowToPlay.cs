using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class HowToPlay : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            HowToPlay layer = new HowToPlay();
            scene.addChild(layer);

            return scene;
        }

        CSprite background;

        CSprite backNormal;
        CSprite backHover;

        Button backButton;


        public HowToPlay()
        {
            background = CSprite.create("BackGroundMenu\\HowToPlay");
            background.setPosition(500, 300);
            this.addChild(background);

            backNormal = CSprite.create("Button\\backButton");
            backHover = CSprite.create("Button\\backButtonHover");

            backButton = new Button(backNormal, backHover, 930, 565);
            this.addChild(backButton);

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
                if (mLastState == ButtonState.Pressed &&
                    backButton.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(MainMenu.scene());
                }
            }
        }
    }
}
