using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace gunShot_FrameWork
{
    class Rocket : CNode
    {
        CSprite rocket;
        public Rectangle rocketRect;
        public Color[] rocketData;
        public float rocketPositionX;
        public float rocketPositionY;



        public Rocket(float rocketPositionX,float rocketPositionY, float rotation)
        {
            this.rocketPositionX = rocketPositionX;
            this.rocketPositionY = rocketPositionY;
            rocket = CSprite.create("bullet");
            //rocket.setScale(0.1f);
            rocket.setPosition(rocketPositionX, rocketPositionY);
            rocket.setRotation(rotation);
            this.addChild(rocket);

            float radian = MathHelper.ToRadians(90 - rocket.getRotation());
            Vx = Vo * Math.Cos(radian);
            Vy = Vo * Math.Sin(radian);
            scaleV = 10;

            rocketData = new Color[(int)rocket.getContentSize().width * (int)rocket.getContentSize().height];
            rocket.getTexture().GetData(rocketData);  
            this.scheduleUpdate();
        }

        float scaleV;
        double Vx, Vy;
        double Vo = 30;

        double g = -9.8;
        public bool isShoot = false;
        public override void update(float dt)
        {          
            if (isShoot == true)
            {
                Vy += g * dt;              

                float xNew = rocket.getPosition().x + (float)Vx * dt * scaleV;
                float yNew = rocket.getPosition().y - (float)Vy * dt * scaleV;

                rocketPositionX = xNew;
                rocketPositionY = yNew;

                //thay đổi viên đạn theo hướng đi.
                float angle = (float)Math.Atan2(-Vy, Vx);
                angle = MathHelper.ToDegrees(angle);
                rocket.setRotation(angle + 90);
            }
            //rocket.setPosition(rocketPositionX, rocketPositionY);
            rocket.setPosition(rocketPositionX, rocketPositionY);
            rocketRect = new Rectangle((int)rocket.getPosition().x - (int)(rocket.getContentSize().width / 2), (int)rocket.getPosition().y - (int)(rocket.getContentSize().height/2),
                (int)rocket.getContentSize().width, (int)rocket.getContentSize().height);
            
        }

        public void explosion()
        {
            Explosion explosion = new Explosion();

            this.parent.addChild(explosion);
            explosion.setPosition(rocket.getPosition().x, rocket.getPosition().y);
            explosion.explode();
            //explore = false;
        }
    }
}
