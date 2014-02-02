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
    class Terrain
    {
      
        GraphicsDevice device;
        GraphicsDeviceManager graphics;
        public Texture2D foregroundTexture;
        Texture2D groundTexture;
        public Color[,] foregroundColorArray;
        public Color[] mapDataColor;
        public Rectangle mapRectangle;
       
        int[] terrainContour;
        public int screenWidth, screenHeight;
        public Terrain(GraphicsDeviceManager graphics)
        {
           
            this.graphics = graphics;

            device = graphics.GraphicsDevice;

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            groundTexture = CDirector.sharedDirector().getContentManager().Load<Texture2D>("ground");
            GenerateTerrainContour();
            CreateForeground();

            mapDataColor = new Color[(int)foregroundTexture.Width * (int)foregroundTexture.Height];
            foregroundTexture.GetData(mapDataColor);
            mapRectangle = new Rectangle(0, 0, (int)foregroundTexture.Width, (int)foregroundTexture.Height);
            foregroundColorArray = TextureTo2DArray(foregroundTexture);
        }


        public void UpdateTerrain()
        {
            mapDataColor = new Color[(int)foregroundTexture.Width * (int)foregroundTexture.Height];
            foregroundTexture.GetData(mapDataColor);
            foregroundColorArray = TextureTo2DArray(foregroundTexture);
        }

        void GenerateTerrainContour()
        {
            terrainContour = new int[screenWidth];
            float offset = screenHeight / 2;
            float peakheight = 100;
            float flatness = 70;
            for (int x = 0; x < screenWidth; x++)
            {
                double height = peakheight / 3 * Math.Sin((float)x / flatness * 2 + 4);
                height += peakheight / 4 * Math.Sin((float)x / flatness * 2 + 2);
                height += peakheight / 5 * Math.Sin((float)x / flatness * 3 + 2);
                height += offset;
                terrainContour[x] = (int)height;

            }


        }

        public void CreateForeground()
        {
            Color[,] groundColors = TextureTo2DArray(groundTexture);
            Color[] foregroundColors = new Color[screenWidth * screenHeight];

            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    if (y > terrainContour[x])
                        foregroundColors[x + y * screenWidth] = groundColors[x % groundTexture.Width, y % groundTexture.Height];
                    else
                        foregroundColors[x + y * screenWidth] = Color.Transparent;
                }
            }

            foregroundTexture = new Texture2D(device, screenWidth, screenHeight, false, SurfaceFormat.Color);
            foregroundTexture.SetData(foregroundColors);        
        }


        public Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        public void AddCrater(Color[,] tex, Matrix mat)
        {
            int width = tex.GetLength(0);
            int height = tex.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (tex[x, y].R > 10)
                    {
                        Vector2 imagePos = new Vector2(x, y);
                        Vector2 screenPos = Vector2.Transform(imagePos, mat);

                        int screenX = (int)screenPos.X;
                        int screenY = (int)screenPos.Y;

                        if ((screenX) > 0 && (screenX < screenWidth))
                            if (terrainContour[screenX] < screenY)
                                terrainContour[screenX] = screenY;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(foregroundTexture, mapRectangle, Color.White);
        }
    }
}
