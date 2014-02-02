using GameFrameWork.Core.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Base
{
    class CAnimationSprite : CSprite
    {

        public static new CAnimationSprite create(string path)
        {
            CAnimationSprite pSprite = new CAnimationSprite();
            pSprite.initWithFile(path);

            return pSprite;
        }


        public static CAnimationSprite create(string path, int width, int height)
        {
            CAnimationSprite pSprite = new CAnimationSprite();
            pSprite.initAnimationSprite(path, width, height);

            return pSprite;
        }

        public static CAnimationSprite create(string path, CRect sourceRect)
        {
            CAnimationSprite pSprite = new CAnimationSprite();
            pSprite.initAnimationSprite(path, sourceRect);

            return pSprite;
        }

        protected Rectangle sourceRect;
        protected CAnimator pAnimator;

        public CAnimationSprite()
        {
            pAnimator = new CAnimator();
            sourceRect = Rectangle.Empty;
        }


        public void initAnimationSprite(string path, int width, int height)
        {
            this.initWithFile(path);
            this.setContentSize(CSize.create(width, height));

            sourceRect = new Rectangle(0, 0, width, height);
        }

        public void initAnimationSprite(string path, CRect sourceRect)
        {
            this.initWithFile(path);
            this.setContentSize(CSize.create(sourceRect.size.width, sourceRect.size.height));

            this.sourceRect = new Rectangle((int)sourceRect.getMinX(), (int)sourceRect.getMinY(),
                (int)sourceRect.size.width, (int)sourceRect.size.height);
        }

        public CAnimator getAnimator()
        {
            return pAnimator;
        }

        public override void draw()
        {
            if (pAnimator.getCurrAnimation() != null)
            {
                sourceRect = pAnimator.getCurrAnimation().getSourceRect();
            }
            else
            {
                if (sourceRect == Rectangle.Empty)
                {
                    sourceRect = new Rectangle(0, 0, (int)this.getContentSize().width, (int)this.getContentSize().height);
                }
            }

            pTexture.draw(this.pRelativeTransformation.position,
                sourceRect, color, alpha,
                this.pRelativeTransformation.rotation,
                this.pRelativeTransformation.scale,
                 (isFlipX ? SpriteEffects.FlipVertically : SpriteEffects.None) | (isFlipY ? SpriteEffects.FlipHorizontally : SpriteEffects.None));
        }
    }
}
