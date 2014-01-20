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
using Microsoft.Xna.Framework.Graphics;

namespace gunShot_FrameWork
{
    class Cannon : CLayer
    {
 
        public CSprite carriage;
        public CSprite cannon;
  
        public Vector2 velocity;
        public Rectangle cannonRect;
        public float cannonRotation = 45;
        public Color[] cannonData;
        //float x = 120, y = 200;

        public CAnimationSprite worm = CAnimationSprite.create("worm_sheet", 64, 64);
        CAnimation run = CAnimation.create(64, 64, 3, 0, 2, false, 0.2f);
        CAnimation idle = CAnimation.create(64, 64, 3, 0, 0, false, 0.2f);

        

        public float carriagePositionX = 200, carriagePositionY = 100;
        public Cannon()
        {

            worm.getAnimator().addAnimation("run", run);
            worm.getAnimator().addAnimation("idle", idle);

            carriage = CSprite.create("worm_sheet");
            //carriage.setColor(Color.Red);
            carriage.setAnchorPoint(CPoint.create(0, 0));
            carriage.setPosition(carriagePositionX, carriagePositionY);
            
            this.addChild(carriage);

            cannonData = new Color[(int)carriage.getContentSize().width * (int)carriage.getContentSize().height];
          
            /// chua doc data ???


            cannon = CSprite.create("cannon");
            cannon.setColor(Color.Red);
            cannon.setPosition(50, 12);
            cannon.setRotation(cannonRotation);
            carriage.addChild(cannon);


            //cannon.getTexture()<< get texture o day, roi get color data
            carriage.getTexture().GetData(cannonData);
          

            //this.scheduleUpdate();
        }
       // bool isPress;
        public void UpdateRect()
        {
            carriagePositionY = (float)Math.Floor((carriagePositionY));
            carriage.setPosition(carriagePositionX, carriagePositionY);
            cannonRect = new Rectangle((int)carriage.getPosition().x, (int)carriage.getPosition().y, (int)carriage.getContentSize().width, (int)carriage.getContentSize().height);
        }

        public CPoint getBulletStartPosition()
        {
            CPoint point = CPoint.create(carriagePositionX + cannon.getPosition().x,
                                        carriagePositionY + cannon.getPosition().y);
            return point;
        }

        public void updateWorm(float dt)
        {
            carriagePositionX += velocity.X;
            carriagePositionY += velocity.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                carriagePositionX--;
                if (!run.isRunning)
                {
                    worm.getAnimator().play("run");
                    worm.setFlipY(false);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                worm.setFlipY(true);
                carriagePositionX++;
                if (!run.isRunning)
                {
                    worm.getAnimator().play("run");
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cannonRotation--;
                if (cannonRotation < -90)
                    cannonRotation = -90;
                cannon.setRotation(cannonRotation);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cannonRotation++;
                if (cannonRotation > 90)
                    cannonRotation = 90;
                cannon.setRotation(cannonRotation);
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{             
            //    isPress = true;
            //}
            //else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isPress == true)
            //{
            //    rocket = new Rocket(cannon.getPosition().x, cannon.getPosition().y, cannonRotation);
            //    rocket.isShoot = true;
            //    carriage.addChild(rocket);
            //    isPress = false;
            //}

            velocity.Y += 0.15f * 0.75f;

            UpdateRect();
            if (carriagePositionY > 500 - carriage.getContentSize().height)
            {
                carriagePositionY = 500 - carriage.getContentSize().height;
                velocity.Y = 0;
            }
        }
    }
}
