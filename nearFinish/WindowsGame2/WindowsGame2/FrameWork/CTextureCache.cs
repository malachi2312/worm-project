using GameFrameWork.Core;
using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Component
{
    class CTextureCache
    {
        private static CTextureCache s_TextureCache = null;

        private Dictionary<string, CTexture> textureMap;

        CTextureCache()
        {
            textureMap = new Dictionary<string, CTexture>();
        }


        public static CTextureCache sharedTextureCache()
        {
            if (s_TextureCache == null)
            {
                s_TextureCache = new CTextureCache();
            }

            return s_TextureCache;
        }

        public CTexture addImage(string imagePath)
        {
            CTexture pTex = null;

            if (textureMap.ContainsKey(imagePath))
            {
                pTex = textureMap[imagePath];
            }
            else
            {
                Texture2D pImage = CDirector.sharedDirector().getContentManager().Load<Texture2D>(imagePath);
                pTex = new CTexture();
                pTex.setImage(pImage);
                pTex.setBatchNode(CDirector.sharedDirector().getSharedSpriteBatch());

                textureMap.Add(imagePath, pTex);
            }

            return pTex;
        }

        public void removeImage(string path)
        {
            if (textureMap.ContainsKey(path))
            {
                CTexture tex = textureMap[path];
                tex.getImage().Dispose();
                textureMap.Remove(path);
            }
           
        }
    }
}
