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
namespace demo5
{
    class Weapon : CNode
    {
        public CSprite weapon;
        float weaponPositionX;
        float weaponPositionY;
        public float weaponRotation = -90;
        public Color[,] weaponColorArray;

        public Weapon(float weaponPositionX, float weaponPositionY, Terrain map)
        {
            this.weaponPositionX = weaponPositionX;
            this.weaponPositionY = weaponPositionY;

            weapon = CSprite.create("gunBlue");
            weapon.setRotation(weaponRotation);
            weapon.setScale(17f / (float)weapon.getContentSize().width);

            this.addChild(weapon);
            weaponColorArray = map.TextureTo2DArray(weapon.getTexture());
        }

        void UpdateWeaponPosition(Player player)
        {
            player.playerPosition.Y = (float)Math.Floor((player.playerPosition.Y));
            weapon.setPosition(player.playerPosition.X, player.playerPosition.Y);
        }

        bool isLeft = true,
             isRight = false;
        public void UpdateWeapon(float dt, Player player)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D) && isRight == false)
            {
                isRight = true;
                isLeft = false;
                weaponRotation = -weaponRotation;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && isLeft == false)
            {
                isLeft = true;
                isRight = false;
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
            }
            weapon.setRotation(weaponRotation);
            UpdateWeaponPosition(player);

        }

    }
}
