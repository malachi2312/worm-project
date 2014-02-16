using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class Weapon : CNode
    {
        public CSprite weapon;
        public float weaponPositionX;
        public float weaponPositionY;
        public float weaponRotation = -90;
        CSprite target;

        public Weapon(float weaponPositionX, float weaponPositionY)
        {
            this.weaponPositionX = weaponPositionX;
            this.weaponPositionY = weaponPositionY;

            weapon = CSprite.create("gunBlue");
            weapon.setRotation(weaponRotation);
            weapon.setScale(17f / (float)weapon.getContentSize().width);

            this.addChild(weapon);

            target = CSprite.create("target");
            target.setAnchorPoint(CPoint.create(-0.1f, 5f));
            target.setPosition(0, 0);
            weapon.addChild(target);

            this.scheduleUpdate();
        }

        public bool isLeft = true
            , isRight = false;
        public void UpdateWeapon(float dt, Worm player)
        {
            player.playerPosition.Y = (float)Math.Floor((player.playerPosition.Y));
            if (player.isMoveAble)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) && isLeft == false && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    isLeft = true;
                    isRight = false;
                    weaponRotation = -weaponRotation;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D) && isRight == false && Keyboard.GetState().IsKeyUp(Keys.A))
                {
                    isRight = true;
                    isLeft = false;
                    weaponRotation = -weaponRotation;
                }

             
                if (isRight)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        weaponRotation--;
                        if (weaponRotation < 0)
                            weaponRotation = 0;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        weaponRotation++;
                        if (weaponRotation > 90)
                            weaponRotation = 90;
                    }
                    weapon.setPosition(player.playerPosition.X + 15, player.playerPosition.Y + 4);
                }

                if (isLeft)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        weaponRotation++;
                        if (weaponRotation > 0)
                            weaponRotation = 0;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        weaponRotation--;
                        if (weaponRotation < -90)
                            weaponRotation = -90;
                    }
                    weapon.setPosition(player.playerPosition.X - 15, player.playerPosition.Y + 4);
                }
                if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                {
                    target.setIsVisible(true);
                }
                else
                    target.setIsVisible(false);
                weapon.setRotation(weaponRotation);
            }
        }
    }
}
