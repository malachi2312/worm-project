using GameFrameWork.Core.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Base
{
    class CSprite : CNode
    {
        protected CTexture pTexture;
        protected Color color;


        public static CSprite create()
        {
            CSprite pSprite = new CSprite();
            if (pSprite != null && pSprite.init())
            {
                return pSprite;
            }

            return null;
        }

        public static CSprite create(string path)
        {
            CSprite pSprite = new CSprite();
            if (pSprite != null && pSprite.initWithFile(path))
            {
                return pSprite;
            }

            return null;
        }

        public CSprite()
        {
            pTexture = null;
            color = Color.White;
        }

        public Texture2D getTexture()
        {
            return pTexture.getImage();
        }

        public void setTexture(CTexture tex)
        {
            pTexture = tex;
            this.setContentSize(CSize.create(pTexture.getTextureWidth(), pTexture.getTextureHeight()));
        }

        public bool initWithFile(string contentPath)
        {
            pTexture = CTextureCache.sharedTextureCache().addImage(contentPath);

            if (pTexture != null)
            {
                this.setContentSize(CSize.create(pTexture.getTextureWidth(), pTexture.getTextureHeight()));
                return true;
            }
            
            return false;
        }

        public override void draw()
        {

            pTexture.draw(this.pRelativeTransformation.position,
                null, color, alpha,
                this.pRelativeTransformation.rotation,
                this.pRelativeTransformation.scale, 
                (isFlipX ? SpriteEffects.FlipVertically : SpriteEffects.None ) | ( isFlipY ? SpriteEffects.FlipHorizontally : SpriteEffects.None ) );
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public Color getColor()
        {
            return this.color;
        }

    }
}
