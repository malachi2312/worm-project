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

namespace demo5
{
    class Button : CLayer
    {
        MouseState mouse;
        CSprite normalButton;
        CSprite hoverButton;
        private Rectangle bound;
        public bool isClick = false;
        
        public Button(CSprite normalButton, CSprite hoverButton, float x,float y)
        {
            this.normalButton = normalButton;
            this.hoverButton = hoverButton;
            normalButton.setPosition(x, y);
            hoverButton.setPosition(x, y);
            this.addChild(normalButton);
            this.addChild(hoverButton);

            this.bound = getRec();

            this.scheduleUpdate();
        }

        public override void update(float dt)
        {
            mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            bool isMouseOver = bound.Contains(mouseX, mouseY);

            if (isMouseOver == true)
            {
                normalButton.setIsVisible(false);
                hoverButton.setIsVisible(true);
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClick = true;
                }                
            }
            else
            {                
                normalButton.setIsVisible(true);
                hoverButton.setIsVisible(false);
            }       
        }
        public Rectangle getRec()
        {
            int w = (int)normalButton.getContentSize().width;
            int h = (int)normalButton.getContentSize().height;
            int x = (int)normalButton.getPosition().x;
            int y = (int)normalButton.getPosition().y;
            return new Rectangle(x - w / 2, y - h / 2, w, h);
        }
    }
}
