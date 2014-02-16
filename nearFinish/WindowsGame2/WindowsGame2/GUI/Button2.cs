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
    class Button2 : CNode
    {
        public CSprite normalButton;
        public CSprite hoverButton;
        public bool isClick;

        public Button2(CSprite normalButton, CSprite hoverButton, float x, float y)
        {
            this.normalButton = normalButton;
            this.hoverButton = hoverButton;
            normalButton.setPosition(x, y);
            hoverButton.setPosition(x, y);
            this.addChild(normalButton);
            this.addChild(hoverButton);

            normalButton.setIsVisible(true);
            hoverButton.setIsVisible(false);

            this.scheduleUpdate();
        }
        bool isTabPress;
        public override void update(float dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                isTabPress = true;
            else if (Keyboard.GetState().IsKeyUp(Keys.Tab) && isTabPress == true)
            {
                isClick = !isClick;
                isTabPress = false;
            }
            if (isClick == true )
            {
                normalButton.setIsVisible(false);
                hoverButton.setIsVisible(true);
            }
            else
            {
                normalButton.setIsVisible(true);
                hoverButton.setIsVisible(false);
            }
            
        }
    }
}
