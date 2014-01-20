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

    public struct ParticleData
    {
        public float BirthTime;
        public float MaxAge;
        public Vector2 OrginalPosition;
        public Vector2 Accelaration;
        public Vector2 Direction;
        public Vector2 Position;
        public float Scaling;
        public Color ModColor;
    }

    class Level:CLayer
    {
        Terrain terrain;
        Cannon worm;
        Rocket rocket;
        bool isPress = false;
        Rectangle screenRect;
        List<ParticleData> particleList = new List<ParticleData>();
        List<Rocket> listRocket = new List<Rocket>();

        public static CScene scene()
        {
            CScene scene = new CScene();
            Level layer = new Level();
            scene.addChild(layer);
            
            return scene;
        }

        public Level()
        {
            terrain = new Terrain();
            worm = new Cannon();
            rocket = new Rocket(worm.cannon.getPosition().x, worm.cannon.getPosition().y, worm.cannonRotation);
            this.addChild(terrain);
            this.addChild(worm);
            this.scheduleUpdate();           
        }

        public override void update(float dt)
        {
            //System.Diagnostics.Trace.WriteLine("update level");
            bool isCollideTexture = false;
            //cannon.update(dt);

            worm.updateWorm(dt);
            //System.Diagnostics.Trace.WriteLine("pos 1: " + worm.carriagePositionY);
            while (IntersectPixel(worm.cannonRect, worm.cannonData, terrain.terrainRect, terrain.terrainColor))
            {
                worm.carriagePositionY -= 1; 
                worm.UpdateRect();
                isCollideTexture = true;
               // System.Diagnostics.Trace.WriteLine("update level");
            }
            if (isCollideTexture)
                worm.velocity.Y = 0;
           // System.Diagnostics.Trace.WriteLine("pos 2: " + worm.carriagePositionY);
            //if (particleList.Count > 0)
            //    UpdateParticles();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isPress = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space) && isPress == true)
            {
                CPoint rkPos = worm.getBulletStartPosition();
                Rocket rk = new Rocket(rkPos.x, rkPos.y, worm.cannonRotation);
                listRocket.Add(rk);
                rk.isShoot = true;
                this.addChild(rk);
                //rk.setPosition(worm.carriage.getPosition());
                isPress = false;
               
            }
            for (int i = listRocket.Count - 1; i >= 0; i--) 
            {
                Rocket rk = listRocket[i];
                if (IntersectPixel(rk.rocketRect, rk.rocketData, terrain.terrainRect, terrain.terrainColor))
                {
                    rk.explosion();

                    //CDirector.sharedDirector().getGraphicsDevice().Textures[0] = null;
                    screenRect = new Rectangle((int)rk.getPosition().x, (int)rk.getPosition().y, 100, 100);
                    Color[] color = new Color[100 * 100];
                    terrain.terrain.getTexture().SetData<Color>(0, screenRect, color, 0, 100 * 100);
                    terrain.terrain.getTexture().GetData<Color>(0, screenRect, color, 0, 100 * 100);

                    for (int j = 0; j < 2500; j++)
                    {
                        color[i].A = 0;
                        color[i].B = 0;
                        color[i].R = 0;
                        color[i].G = 0;
                    }
                    rk.unscheduleUpdate();
                    rk.stopAllAction();
                    rk.removeFromParentWithCleanUp();

                    listRocket.Remove(rk);
                }
            }
            
        }

        //private void UpdateParticles()
        //{

        //    float now;
        //    now = + 2f;
        //    for (int i = particleList.Count - 1; i >= 0; i--)
        //    {
        //        ParticleData particle = particleList[i];
        //        float timeAlive = now - particle.BirthTime;

        //        if (timeAlive > particle.MaxAge)
        //        {
        //            particleList.RemoveAt(i);
        //        }
        //        else
        //        {
        //            float relAge = timeAlive / particle.MaxAge;
        //            particle.Position = 0.5f * particle.Accelaration * relAge * relAge + particle.Direction * relAge + particle.OrginalPosition;

        //            float invAge = 1.0f - relAge;
        //            particle.ModColor = new Color(new Vector4(invAge, invAge, invAge, invAge));

        //            Vector2 positionFromCenter = particle.Position - particle.OrginalPosition;
        //            float distance = positionFromCenter.Length();
        //            particle.Scaling = (50.0f + distance) / 200.0f;

        //            particleList[i] = particle;
        //        }
        //    }
        //}

      
        static bool IntersectPixel(Rectangle rect1, Color[] colorData1,
                            Rectangle rect2, Color[] colorData2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            //System.Diagnostics.Trace.WriteLine("top = " + top + ", bottom = " + bottom + ", left = " + left + ", right = " + right);

            for (int y = bottom - 1; y >= top; y--)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = colorData1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
                    Color color2 = colorData2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];
                   // System.Diagnostics.Trace.WriteLine("color 1 = " + color1.ToString() + ", color 2 = " + color2.ToString());

                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }
            }
            return false;
        }
    }
}
