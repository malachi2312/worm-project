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
namespace demo5
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

    class Explosions
    {
        Texture2D explosionTexture;
        public List<ParticleData> listParitcle;
        Random randomizer = new Random();
        Color[,] explosionColorArray;
        Terrain map;
        public Explosions(Terrain map)
        {
            this.map = map;
            explosionTexture = CDirector.sharedDirector().getContentManager().Load<Texture2D>("explosion");
            listParitcle = new List<ParticleData>();
            explosionColorArray = map.TextureTo2DArray(explosionTexture);
        }

        public void CheckParticle(GameTime gameTime)
        {
            if(listParitcle.Count > 0)
                UpdateParticles(gameTime);
        }
        void UpdateParticles(GameTime gameTime)
        {
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            for (int i = listParitcle.Count - 1; i >= 0; i--)
            {
                ParticleData particle = listParitcle[i];
                float timeAlive = now - particle.BirthTime;

                if (timeAlive > particle.MaxAge)
                {
                    listParitcle.RemoveAt(i);
                }
                else
                {
                    float relAge = timeAlive / particle.MaxAge;
                    particle.Position = 0.5f * particle.Accelaration * relAge * relAge + particle.Direction * relAge + particle.OrginalPosition;

                    float invAge = 1.0f - relAge;
                    particle.ModColor = new Color(new Vector4(invAge, invAge, invAge, invAge));

                    Vector2 positionFromCenter = particle.Position - particle.OrginalPosition;
                    float distance = positionFromCenter.Length();
                    particle.Scaling = (50.0f + distance) / 200.0f;

                    listParitcle[i] = particle;
                }
            }
        }

        public void AddExplosion(Vector2 explosionPos, int numberOfParticles, float size, float maxAge,GameTime gameTime)
        {
            for (int i = 0; i < numberOfParticles; i++)
                AddExplosionParticle(explosionPos, size, maxAge,gameTime);

            ProcessCrater(explosionPos, size);
           
        }

         void ProcessCrater(Vector2 explosionPos, float size)
        {
            float rotation = (float)randomizer.Next(10);
            Matrix mat = Matrix.CreateTranslation(-explosionTexture.Width / 2, -explosionTexture.Height / 2, 0)
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(size / (float)explosionTexture.Width * 2.0f)
                * Matrix.CreateTranslation(explosionPos.X, explosionPos.Y, 0);
            map.AddCrater(explosionColorArray, mat);
        }

        void AddExplosionParticle(Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime)
        {
            ParticleData particle = new ParticleData();

            particle.OrginalPosition = explosionPos;
            particle.Position = particle.OrginalPosition;

            particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            particle.MaxAge = maxAge;
            particle.Scaling = 0.25f;
            particle.ModColor = Color.White;

            float particleDistance = (float)randomizer.NextDouble() * explosionSize;
            Vector2 displacement = new Vector2(particleDistance, 0);
            float angle = MathHelper.ToRadians(randomizer.Next(360));
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

            particle.Direction = displacement * 2f;
            particle.Accelaration =  -particle.Direction;

            listParitcle.Add(particle);
        }

        public void DrawExplosion(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            for (int i = 0; i < listParitcle.Count; i++)
            {
                ParticleData particle = listParitcle[i];
                spriteBatch.Draw(explosionTexture, particle.Position, null, particle.ModColor, i, new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }
            spriteBatch.End();
        }
    }
}
