using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame2
{
    class Level1 : CLayer
    {
        public static CScene scene()
        {
            CScene scene = new CScene();
            Level1 layer = new Level1();
            scene.addChild(layer);

            return scene;
        }

        Terrain map;

        Worm player1;
        Weapon weapon1;

        Worm player2;
        Weapon weapon2;

        List<Bullet> listBullets = new List<Bullet>();

        List<Worm> listWorms = new List<Worm>();
        List<Weapon> listWeapons = new List<Weapon>();

        bool isSpacePress;
        //shotBar
        CSprite powerBar;
        CSprite powerPoint;
        CSprite movePoint;
        double Vpoint;
        //escape
        Escape escape;

        //chose bullet
        Button2 Bullet1Button;
        Button2 Bullet2Button;

        CSprite bullet1Normal;
        CSprite bullet1Hover;

        CSprite bullet2Normal;
        CSprite bullet2Hover;

        CSprite gameOver;

        SoundEffect soundEffect;

        public Level1()
        {
            map = new Terrain("Map\\DinoMap", "Map\\level1");
            this.addChild(map);

            player1 = new Worm("player1");
            player1.playerPosition.X = 200;
            weapon1 = new Weapon(player1.playerPosition.X - 10, player1.playerPosition.Y + 4);

            player2 = new Worm("player2");
            player2.playerPosition.X = 500;
            weapon2 = new Weapon(player2.playerPosition.X - 10, player2.playerPosition.Y + 4);

            listWeapons.Add(weapon1);
            listWeapons.Add(weapon2);

            listWorms.Add(player1);
            listWorms.Add(player2);

            this.addChild(weapon1);
            this.addChild(weapon2);

            this.addChild(player1);
            this.addChild(player2);

            //powerBar
            powerBar = CSprite.create("ToolBar\\powerBar");
            powerBar.setAnchorPoint(CPoint.create(0, 0));
            powerBar.setPosition(100, 522);
            this.addChild(powerBar);

            powerPoint = CSprite.create("ToolBar\\power");
            powerPoint.setAnchorPoint(CPoint.create(0, 0));
            powerPoint.setPosition(108, 13);
            powerPoint.setColor(Color.Red);
            powerPoint.setScaleX(0f);
            powerBar.addChild(powerPoint);

            movePoint = CSprite.create("Toolbar\\move");
            movePoint.setAnchorPoint(CPoint.create(0, 0));
            movePoint.setPosition(107, 59);
            movePoint.setColor(Color.DarkBlue);
            powerBar.addChild(movePoint);

            //soundEffect
            soundEffect = CDirector.sharedDirector().getContentManager().Load<SoundEffect>("Sound\\soundgun");

            //escape
            escape = new Escape();
            this.addChild(escape);

            //bullet button
            bullet1Normal = CSprite.create("ToolBar\\bullet1Normal");
            bullet1Hover = CSprite.create("ToolBar\\bullet1Hover");

            bullet2Normal = CSprite.create("ToolBar\\bullet2Normal");
            bullet2Hover = CSprite.create("ToolBar\\bullet2Hover");

            Bullet1Button = new Button2(bullet1Normal, bullet1Hover, 15, 539);
            Bullet2Button = new Button2(bullet2Normal, bullet2Hover, 15, 578);

            Bullet1Button.isClick = true;

            this.addChild(Bullet1Button);
            this.addChild(Bullet2Button);

            gameOver = CSprite.create("gameover");
            gameOver.setPosition(500, 100);
            gameOver.setIsVisible(false);

            this.addChild(gameOver);

            this.scheduleUpdate();
        }

        int i = 0;

        float d;
        public override void update(float dt)
        {
            updateEscape(dt);

            if (pause == true)
            {
                escape.setIsVisible(true);
                escape.isClickAble = true;
            }
            else
            {
                escape.setIsVisible(false);
                escape.isClickAble = false;
                updateLever(dt);
            }

            for (int q = 0; q < listWorms.Count; q++)
            {
                if (listWorms[q].healthPoint <= 0)
                {
                    gameOver.setIsVisible(true);
                    d += dt;
                    if (d > 3)
                    {
                        this.unscheduleUpdate();
                        CDirector.sharedDirector().replaceScene(GameOver.scene());
                    }

                }
            }
            
        }
  
        bool pause = false;
        bool isEscapePress = false;
        bool isShot = false;

        private void updateEscape(float dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isEscapePress = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape) && isEscapePress == true)
            {
                pause = !pause;
                isEscapePress = false;
            }
        }
        //processWorm
        public void updateLever(float dt)
        {
            for (int q = 0; q < listWorms.Count; q++)
            {
                ProcessBullet(dt);

                bool isCollideTexture = false;

                //if (i > listWorms.Count - 1)
                //    i = 0;

                if (q == i)
                {
                    listWorms[i].isMoveAble = true;
                    listWeapons[q].setIsVisible(true);
                }
                else
                {
                    listWorms[q].isMoveAble = false;
                    listWeapons[q].setIsVisible(false);
                }

                listWorms[q].UpdatePlayer(dt);
                listWeapons[q].UpdateWeapon(dt, listWorms[q]);

                if (IntersectPixel(listWorms[q].playerRectangle1, listWorms[q].playerColorData, map.mapRect, map.mapColor))
                {
                    listWorms[q].playerPosition.Y--;
                    listWorms[q].UpdateRect();
                    listWeapons[q].UpdateWeapon(dt, listWorms[q]);
                    isCollideTexture = true;
                }
                if (IntersectPixel(listWorms[q].playerRectangle3, listWorms[q].playerColorData, map.mapRect, map.mapColor))
                {
                    listWorms[q].playerPosition.X -= 1f;
                }
                if (IntersectPixel(listWorms[q].playerRectangle4, listWorms[q].playerColorData, map.mapRect, map.mapColor))
                {
                    listWorms[q].playerPosition.X += 1f;
                }
                if (isCollideTexture)
                    listWorms[q].velocity.Y = 0;
            }
            movePoint.setScaleX((float)listWorms[i].movePoint / 30.0f);
        }
        //processBullet
        int bulletStyle;
        bool explore = false;
        void ProcessBullet(float dt)
        {
            
            if (Bullet1Button.isClick)
                bulletStyle = 1;
            if (Bullet2Button.isClick)
                bulletStyle = 2;

            
            if (isShot == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    isSpacePress = true;
                    Vpoint += 0.3f;
                    if (Vpoint > 50)
                        Vpoint = 50;
                }

                else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isSpacePress == true && bulletStyle == 1)
                {
                    Bullet bullet = new Rocket(listWorms[i], listWeapons[i], Vpoint);
                    listBullets.Add(bullet);
                    bullet.bulletFlying = true;
                    this.addChild(bullet);
                    isSpacePress = false;
                    Vpoint = 0;
                    isShot = true;

                    soundEffect.Play();
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isSpacePress == true && bulletStyle == 2)
                {
                    Bullet bullet = new Rocket2(listWorms[i], listWeapons[i], Vpoint);
                    listBullets.Add(bullet);
                    bullet.bulletFlying = true;
                    this.addChild(bullet);
                    isSpacePress = false;
                    Vpoint = 0;
                    isShot = true;

                    soundEffect.Play();
                }
            }

            for (int k = listBullets.Count - 1; k >= 0; k--)
            {
                Bullet bullet = listBullets[k];
                bool bulletOutOfScreen = CheckOutOfScreen(bullet);
                if (bulletOutOfScreen)
                {
                    bullet.unscheduleUpdate();
                    bullet.stopAllAction();
                    bullet.removeFromParentWithCleanUp();
                    listBullets.Remove(bullet);
                    i++;
                    isShot = false;
                }
                //check intersect bullet
                for (int q = 0; q < listWorms.Count; q++)
                {
                    if (IntersectPixel(bullet.bulletRectangle, bullet.bulletColorData, listWorms[q].playerRectangle2, listWorms[q].playerColorData)
                        || IntersectPixel(bullet.bulletRectangle, bullet.bulletColorData, map.mapRect, map.mapColor))
                    {
                        AddCrater(bullet);
                        i++;
                        isShot = false;

                        explore = true;
                    }
                    if (IntersectPixel(bullet.bulletRectangle, bullet.bulletColorData, listWorms[q].playerRectangle2, listWorms[q].playerColorData))
                    {
                        listWorms[q].healthPoint -= 20;
                    }                  
                }
                //change Turn and return movePoint
                if (i > listWorms.Count - 1)
                    i = 0;
                if (isShot == false)
                    listWorms[i].movePoint = 30;

                if (explore == true)
                {
                    bullet.unscheduleUpdate();
                    bullet.stopAllAction();
                    bullet.removeFromParentWithCleanUp();
                    listBullets.Remove(bullet);
                    explore = false;
                }
            }
            //set powerPoint and movePoint
            powerPoint.setScaleX((float)Vpoint / 50.0f);
            movePoint.setScaleX((float)listWorms[i].movePoint / 30.0f);            
        }

        bool CheckOutOfScreen(Bullet bullet)
        {
            bool bulletOutOfScreen = bullet.bulletPosition.Y > 600;
            bulletOutOfScreen |= bullet.bulletPosition.X < 0;
            bulletOutOfScreen |= bullet.bulletPosition.X > 1000;
            return bulletOutOfScreen;
        }

        void AddCrater(Bullet bullet)
        {
            bullet.Explosion();
            Rectangle screenRect = new Rectangle((int)bullet.bulletPosition.X - 50, (int)bullet.bulletPosition.Y - 50, 100, 100);
            Rectangle rect = new Rectangle(0, 0, 100, 100);

            Color[] color1 = new Color[100 * 100];
            Color[] color2 = new Color[100 * 100];

            map.map.getTexture().GetData<Color>(0, screenRect, color1, 0, 100 * 100);
            map.crater.GetData<Color>(0, rect, color2, 0, 100 * 100);

            int leng = 100 * 100;
            for (int j = 0; j < leng; j++)
            {
                if (color2[j].R == 0 && color1[j].A != 0)
                {
                    color1[j].A = 0;
                    color1[j].B = 0;
                    color1[j].R = 0;
                    color1[j].G = 0;
                }
            }

            map.map.getTexture().SetData<Color>(0, screenRect, color1, 0, 100 * 100);
            map.mapColor = new Color[(int)map.map.getContentSize().width * (int)map.map.getContentSize().height];
            map.map.getTexture().GetData(map.mapColor);           
        }


        static bool IntersectPixel(Rectangle rect1, Color[] colorData1,
                           Rectangle rect2, Color[] colorData2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = bottom - 1; y >= top; y--)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = colorData1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = colorData2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }
            }
            return false;
        }
    }
}
