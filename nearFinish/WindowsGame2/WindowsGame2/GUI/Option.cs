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
    class Option : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            Option layer = new Option();
            scene.addChild(layer);

            return scene;
        }

        CSprite background;
        CSprite volumeBar;
        CSprite volumeBar1;

        CSprite backNormal;
        CSprite backHover;

        Button backButton;


        public Option()
        {
            background = CSprite.create("BackGroundMenu\\Option");
            background.setPosition(500, 300);
            this.addChild(background);

            backNormal = CSprite.create("Button\\backButton");
            backHover = CSprite.create("Button\\backButtonHover");
            backNormal.setColor(Color.White);

            backButton = new Button(backNormal, backHover, 800, 500);
            this.addChild(backButton);

            volumeBar = CSprite.create("volumeBar");
            volumeBar.setPosition(300, 300);
            this.addChild(volumeBar);

            volumeBar1 = CSprite.create("volumeBar");
            volumeBar1.setColor(Color.Red);
            volumeBar1.setAnchorPoint(CPoint.create(0, 0));
            volumeBar1.setPosition(150.5f, 290);
            this.addChild(volumeBar1);

            point = 300;
            pointper = 1;

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
            volumeControl();
        }

        MouseState mouse;
        float point;
        float pointper;
        public void volumeControl()
        {
            int w = (int)volumeBar.getContentSize().width;
            int h = (int)volumeBar.getContentSize().height;
            int x = (int)volumeBar.getPosition().x;
            int y = (int)volumeBar.getPosition().y;
            Rectangle volumeRect = new Rectangle(x - w / 2, y - h / 2, w, h);

            mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            bool isMouseOver = volumeRect.Contains(mouseX, mouseY);
            if (isMouseOver == true && mouse.LeftButton == ButtonState.Pressed)
            {
                
                    point = mouseX - (float)(x - (float)w/2) + 1;
                
            }
            pointper = point / (float)w;
            volumeBar1.setScaleX(pointper);
        }
    }
}
