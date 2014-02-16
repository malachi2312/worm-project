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
using Microsoft.Xna.Framework;

namespace WindowsGame2
{
    class Escape : CNode
    {
        Button backToMainMenu;
        Button backToMapMenu;
        Button Quit;

        CSprite backToMainMenuNormal;
        CSprite backToMainMenuHover;

        CSprite backToMapMenuNormal;
        CSprite backToMapMenuHover;

        CSprite quitNormal;
        CSprite quitHover;

        public bool isClickAble = true;
        public Escape()
        {
            backToMainMenuNormal = CSprite.create("Escape\\backToMainMenuNormal");
            backToMainMenuHover = CSprite.create("Escape\\backToMainMenuHover");

            backToMapMenuNormal = CSprite.create("Escape\\backToMapMenuNormal");
            backToMapMenuHover = CSprite.create("Escape\\backToMapMenuHover");

            quitNormal = CSprite.create("Escape\\QuitNormal");
            quitHover = CSprite.create("Escape\\QuitHover");

            backToMainMenu = new Button(backToMainMenuNormal, backToMainMenuHover, 470, 300);
            backToMapMenu = new Button(backToMapMenuNormal, backToMapMenuHover, 470, 400);
            Quit = new Button(quitNormal, quitHover, 470, 500);

            this.addChild(backToMainMenu);
            this.addChild(backToMapMenu);
            this.addChild(Quit);

            this.scheduleUpdate();
        }

        private ButtonState mLastState = ButtonState.Released;

        public override void update(float dt)
        {
            if (isClickAble)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    mLastState = ButtonState.Pressed;
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if (mLastState == ButtonState.Pressed && backToMainMenu.isClick == true)
                    {
                        this.unscheduleUpdate();
                        CDirector.sharedDirector().replaceScene(MainMenu.scene());
                    }
                    else if (mLastState == ButtonState.Pressed && backToMapMenu.isClick == true)
                    {
                        this.unscheduleUpdate();
                        CDirector.sharedDirector().replaceScene(MapMenu.scene());
                    }
                    else if (mLastState == ButtonState.Pressed && Quit.isClick == true)
                    {
                        this.unscheduleUpdate();
                        MainMenu.exit = true;
                    }
                }
            }
        }
    }
}
