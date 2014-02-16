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
    class Rocket2 : Bullet
    {
        Explosion explosion;
        public Rocket2(Worm player, Weapon weapon, double Vo)
        {
            this.bullet = CSprite.create("FireBall");
            if (weapon.isLeft)
            {
                CPoint rPos = player.getBulletStartPosition();
                bullet.setPosition(rPos.x - 25, rPos.y + 3);
            }
            if (weapon.isRight)
            {
                CPoint rPos = player.getBulletStartPosition();
                bullet.setPosition(rPos.x + 25, rPos.y + 3);
            }
            bullet.setRotation(weapon.weaponRotation);
            bulletAngle = bullet.getRotation();
            bullet.setScale(bulletScaling);


            this.addChild(bullet);

            float radian = MathHelper.ToRadians(90 - bullet.getRotation());
            Vx = Vo * Math.Cos(radian);
            Vy = Vo * Math.Sin(radian);

            this.scheduleUpdate();
            bulletColorData = new Color[(int)bullet.getContentSize().width * (int)bullet.getContentSize().height];
            bullet.getTexture().GetData(bulletColorData);


            this.scheduleUpdate();
            this.damage = 15;

            explosion = new Explosion();
        }

        bool isPress = false;
        public void UpdateBullet(float dt)
        {
            if (bulletFlying == true)
            {              
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    isPress = true;
                else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isPress == true)
                {
                    Vx = 0;
                    Vy = 0;
                    isPress = false;
                }
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
            bulletRectangle = new Rectangle((int)bulletPosition.X - (int)(bullet.getContentSize().width / 10), (int)bulletPosition.Y - (int)(bullet.getContentSize().height/10 - 10),
                (int)bullet.getContentSize().width/10, (int)bullet.getContentSize().height/10);
        }

        public override void update(float dt)
        {
            if (bulletFlying == true)
            {
                UpdateBullet(dt);
            }
        }

        public override void Explosion()
        {
            this.parent.addChild(explosion);
            explosion.setPosition(bullet.getPosition().x, bullet.getPosition().y + 10);
            explosion.explode();
        }
    }
}
