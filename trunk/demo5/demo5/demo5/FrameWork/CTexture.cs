using GameFrameWork.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CTexture : CComponent
    {
        protected Texture2D pImage;
        protected SpriteBatch pBatchNode;
       
        public CTexture()
        {
            pImage = null;
            pBatchNode = null;
        }

        public int getTextureWidth()
        {
            return pImage.Width;
        }

        public int getTextureHeight()
        {
            return pImage.Height;
        }

        public void setImage(Texture2D tex)
        {
            pImage = tex;
        }

        public void setBatchNode(SpriteBatch pBatchNode)
        {
            this.pBatchNode = pBatchNode;
        }

        public Texture2D getImage()
        {
            return pImage;
        }


        public void draw( Vector2 pos , Rectangle? sourceRect , Color color , float alpha , float rotation , Vector2 scale , SpriteEffects effect  )
        {
            try
            {
                pBatchNode.Draw(pImage, pos , sourceRect , color * alpha, rotation, Vector2.Zero, scale , effect, 0);
            }
            catch (Exception e)
            {
                CLog.create(e.Message);
            }
        }


    }
}
