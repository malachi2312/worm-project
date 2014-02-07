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
    class MapMenu : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            MapMenu layer = new MapMenu();
            scene.addChild(layer);

            return scene;
        }

        CSprite map1_Normal;
        CSprite backNormal;

        CSprite map1_Hover;
        CSprite backHover;
        CSprite background;

        Button map1;
        Button backButton;
        public MapMenu()
        {
            background = CSprite.create("MapMenu\\MapMenu");
            background.setPosition(500, 300);
            this.addChild(background);


            map1_Normal = CSprite.create("MapMenu\\map1_Normal");
            map1_Hover = CSprite.create("MapMenu\\map1_Hover");

            backNormal = CSprite.create("backButton");
            backHover = CSprite.create("backButtonHover");

            map1 = new Button(map1_Normal, map1_Hover, 250, 250);
            backButton = new Button(backNormal, backHover, 800, 500);

            this.addChild(map1);
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
                        backButton.isClick == true )
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(MainMenu.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                       map1.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(Level.scene());
                }
            }
        }

    }
}
