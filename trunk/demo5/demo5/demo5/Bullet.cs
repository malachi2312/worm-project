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
    class Bullet:CNode
    {
        public CSprite bullet;
        public float bulletScaling = 0.15f;

        float bulletAngle;
        public bool bulletFlying = false;
        public Vector2 bulletPosition;
        public Color[,] bulletColorArray;
        double Vx, Vy;
        double Vo = 30;
        double g = -9.8;
        float scaleV = 10;
        public float angle;

        public Bullet(Player player, Weapon weapon, Terrain map)
        {
           
            bullet = CSprite.create("rocket");
           
            bullet.setPosition(player.playerPosition.X, player.playerPosition.Y);  
            bullet.setRotation(weapon.weaponRotation);
            bulletAngle = bullet.getRotation();
            bullet.setScale(bulletScaling);
        

            this.addChild(bullet);
          
            float radian = MathHelper.ToRadians(90 - bullet.getRotation());
            Vx = Vo * Math.Cos(radian);
            Vy = Vo * Math.Sin(radian);

            bulletColorArray = map.TextureTo2DArray(bullet.getTexture());
            this.scheduleUpdate();
        }

        public void UpdateBullet(float dt)
        {
            if (bulletFlying == true)
            {
                Vy += g * dt;

                float xNew = bullet.getPosition().x + (float)Vx * dt * scaleV;
                float yNew = bullet.getPosition().y - (float)Vy * dt * scaleV;

                bulletPosition.X = xNew;
                bulletPosition.Y = yNew;

                angle = (float)Math.Atan2(-Vy, Vx);
                angle = MathHelper.ToDegrees(angle);
                bullet.setRotation(angle + 90);

                bullet.setPosition(bulletPosition.X, bulletPosition.Y);     
            }
          
                 
        }

        public override void update(float dt)
        {
            if (bulletFlying == true)
            {
                UpdateBullet(dt);
                UpdateBullet(dt);
            }
        }

    }
}
