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

namespace demo5
{
    class About : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            About layer = new About();
            scene.addChild(layer);

            return scene;
        }

        CSprite background;

        CSprite backNormal;
        CSprite backHover;

        Button backButton;


        public About()
        {
            background = CSprite.create("About");
            background.setPosition(500, 300);
            this.addChild(background);

            backNormal = CSprite.create("backButton");
            backHover = CSprite.create("backButtonHover");

            backButton = new Button(backNormal, backHover, 800, 500);
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
