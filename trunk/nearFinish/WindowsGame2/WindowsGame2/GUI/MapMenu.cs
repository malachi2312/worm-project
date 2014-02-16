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
        CSprite map2_Normal;
        CSprite map3_Normal;

        CSprite map1_Hover;
        CSprite map2_Hover;
        CSprite map3_Hover;

        CSprite backHover;
        CSprite background;

        Button map1;
        Button map2;
        Button map3;

        Button backButton;
        public MapMenu()
        {
            background = CSprite.create("BackGroundMenu\\MapMenu");
            background.setPosition(500, 300);
            this.addChild(background);


            map1_Normal = CSprite.create("Button\\map1_Normal");
            map1_Hover = CSprite.create("Button\\map1_Hover");

            map2_Normal = CSprite.create("Button\\map2_Normal");
            map2_Hover = CSprite.create("Button\\map2_Hover");

            map3_Normal = CSprite.create("Button\\map3_Normal");
            map3_Hover = CSprite.create("Button\\map3_Hover");

            backNormal = CSprite.create("Button\\backButton");
            backHover = CSprite.create("Button\\backButtonHover");

            map1 = new Button(map1_Normal, map1_Hover, 250, 250);
            map2 = new Button(map2_Normal, map2_Hover, 500, 250);
            map3 = new Button(map3_Normal, map3_Hover, 750, 250);
            backButton = new Button(backNormal, backHover, 930, 565);

            this.addChild(map1);
            this.addChild(map2);
            this.addChild(map3);
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
                    CDirector.sharedDirector().replaceScene(Level1.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                       map2.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(Level2.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                      map3.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(Level3.scene());
                }
            }
        }

    }
}
