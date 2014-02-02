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
    //public struct ParticleData
    //{
    //    public float BirthTime;
    //    public float MaxAge;
    //    public Vector2 OrginalPosition;
    //    public Vector2 Accelaration;
    //    public Vector2 Direction;
    //    public Vector2 Position;
    //    public float Scaling;
    //    public Color ModColor;
    //}

    class Level:CNode
    {
        Terrain map;
        Player player;
        Weapon weapon;
        List<Bullet> listBullets = new List<Bullet>();
        Texture2D explosionTexture;
        List<ParticleData> listParitcle = new List<ParticleData>();
        Explosions explosion;
       // Random randomizer = new Random();
        bool isPress = false;
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
            explosion = new Explosions();
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

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isPress = true;
            }

            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isPress == true)
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
                if(bullet.bulletFlying)
                {
                  
                    CheckCollision(dt, bullet);
                }
                
            }           
        }

        //private void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime)
        //{
        //    for (int i = 0; i < numberOfParticles; i++)
        //        AddExplosionParticle(explosionPos, size, maxAge,gameTime);
        //}

        //private void AddExplosionParticle(Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime)
        //{
        //    ParticleData particle = new ParticleData();

        //    particle.OrginalPosition = explosionPos;
        //    particle.Position = particle.OrginalPosition;

        //    particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
        //    particle.MaxAge = maxAge;
        //    particle.Scaling = 0.25f;
        //    particle.ModColor = Color.White;

        //    float particleDistance = (float)randomizer.NextDouble() * explosionSize;
        //    Vector2 displacement = new Vector2(particleDistance, 0);
        //    float angle = MathHelper.ToRadians(randomizer.Next(360));
        //    displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

        //    particle.Direction = displacement;
        //    particle.Accelaration = 3.0f * particle.Direction;

        //    listParitcle.Add(particle);
        //}

        public override void draw()
        {
            map.Draw(CDirector.sharedDirector().getSharedSpriteBatch());

        }

        public void drawExplosion()
        {
            explosion.DrawExplosion(CDirector.sharedDirector().getSharedSpriteBatch());
        }

        //public void DrawExplosion()
        //{
        //    CDirector.sharedDirector().getSharedSpriteBatch().Begin(SpriteSortMode.Deferred, BlendState.Additive);
        //    for (int i = 0; i < listParitcle.Count; i++)
        //    {
        //        ParticleData particle = listParitcle[i];
                
        //        CDirector.sharedDirector().getSharedSpriteBatch().Draw(explosionTexture, particle.Position, null,
        //            particle.ModColor, i, new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
               
        //    }
        //    CDirector.sharedDirector().getSharedSpriteBatch().End();
        //}
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

        //Vector2 CheckPlayerCollision()
        //{
        //    Matrix bulletMat = Matrix.CreateTranslation(-42, -240, 0) * Matrix.CreateRotationZ(bullet.angle) * Matrix.CreateScale(bullet.bulletScaling) * Matrix.CreateTranslation(bullet.bulletPosition.X, bullet.bulletPosition.Y, 0);

        //}

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
              
                explosion.AddExplosion(terrainCollisionPoint, 4, 30f, 1000f, CDirector.sharedDirector().getShareGameTime());
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
