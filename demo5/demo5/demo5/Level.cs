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
    class Level:CNode
    {
        Terrain map;
        Player player;
        Weapon weapon;
        List<Bullet> listBullets = new List<Bullet>();
        Texture2D explosionTexture;
        List<ParticleData> listParitcle = new List<ParticleData>();
        Explosions explosion;
     
        bool isPress;
        public static CScene scene()
        {
            CScene scene = new CScene();
            Level layer = new Level();
            scene.addChild(layer);
            
            return scene;
        }
        public Level()
        {
            map = new Terrain(CDirector.sharedDirector().getSharedGraphicsDeviceManager());
            player = new Player(map);
            weapon = new Weapon(player.playerPosition.X + 10, player.playerPosition.Y, map);
            explosionTexture = CDirector.sharedDirector().getContentManager().Load<Texture2D>("explosion");
            explosion = new Explosions(map);
            this.addChild(weapon);
            this.addChild(player);  
            this.scheduleUpdate();
        }

        public override void update(float dt)
        {
            bool isCollideTexture = false;
            player.UpdatePlayer(dt);
            weapon.UpdateWeapon(dt, player);
            while (IntersectPixel(player.playerRectangle, player.playerColorData, map.mapRectangle, map.mapDataColor))
            {
                player.playerPosition.Y--;
                player.UpdateRect();
                weapon.UpdateWeapon(dt, player);
                isCollideTexture = true;
            }

            if (isCollideTexture)
                player.velocity.Y = 0;
            if (explosion.listParitcle.Count == 0)
                ProcessBullet(dt);            
          
            explosion.CheckParticle(CDirector.sharedDirector().getShareGameTime());
          
           
        }
        void ProcessBullet(float dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isPress = true;
            }

             if (Keyboard.GetState().IsKeyUp(Keys.Space) && isPress == true)
            {
                Bullet bullet = new Bullet(player, weapon, map);
                listBullets.Add(bullet);
                bullet.bulletFlying = true;
                this.addChild(bullet);
                isPress = false;
            }

            for (int i = listBullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = listBullets[i];
                if (bullet.bulletFlying)
                {
                    CheckCollision(dt, bullet);
                  
                }

            }

          
        }
      
        public override void draw()
        {
            map.Draw(CDirector.sharedDirector().getSharedSpriteBatch());

        }

        public void drawExplosion()
        {
            explosion.DrawExplosion(CDirector.sharedDirector().getSharedSpriteBatch());
        }

        Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);

            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }


        Vector2 CheckTerrainCollision(Bullet bullet)
        {
            Matrix bulletMat = Matrix.CreateTranslation(-42, -240, 0) 
                * Matrix.CreateRotationZ(bullet.getRotation()) * Matrix.CreateScale(bullet.bulletScaling) 
                * Matrix.CreateTranslation(bullet.bulletPosition.X, bullet.bulletPosition.Y, 0);
            Matrix terrainMat = Matrix.Identity;
            Vector2 terrainCollisionPoint = TexturesCollide(bullet.bulletColorArray, bulletMat, map.foregroundColorArray, terrainMat);
            return terrainCollisionPoint;
          
        }

       

        bool CheckOutOfScreen(Bullet bullet)
        {
            bool bulletOutOfScreen = bullet.bulletPosition.Y > map.screenHeight;
            bulletOutOfScreen |= bullet.bulletPosition.X < 0;
            bulletOutOfScreen |= bullet.bulletPosition.X > map.screenWidth;
            return bulletOutOfScreen;
        }


        

        void CheckCollision(float dt, Bullet bullet)
        {
            Vector2 terrainCollisionPoint = CheckTerrainCollision(bullet);
            bool bulletOutOfScreen = CheckOutOfScreen(bullet);
            if (terrainCollisionPoint.X > -1)
            {
                bullet.unscheduleUpdate();
                bullet.stopAllAction();
                bullet.removeFromParentWithCleanUp();
                listBullets.Remove(bullet);
              
                explosion.AddExplosion(terrainCollisionPoint, 4, 40f, 1000f, CDirector.sharedDirector().getShareGameTime());
                map.CreateForeground();
                map.UpdateTerrain();
            }

            if (bulletOutOfScreen)
            {
                bullet.unscheduleUpdate();
                bullet.stopAllAction();
                bullet.removeFromParentWithCleanUp();
                listBullets.Remove(bullet);
            }
           
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
