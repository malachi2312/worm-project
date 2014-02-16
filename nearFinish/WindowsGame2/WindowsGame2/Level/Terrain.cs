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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    class Terrain: CNode
    {
        public CSprite map;
        public Texture2D crater;
        public Color[] mapColor;
        public Rectangle mapRect;
        CSprite backGround;
        Song BGMusic;
        public Terrain(String path, String BG)
        {
            BGMusic = CDirector.sharedDirector().getContentManager().Load<Song>("Sound\\BGMusic");

            MediaPlayer.Play(BGMusic);

            MediaPlayer.Volume = 0.5f;

            MediaPlayer.IsRepeating = true;

            CSprite terrain = CSprite.create(path);
            Color[] terrainColor = new Color[(int)terrain.getContentSize().width * (int)terrain.getContentSize().height];
            terrain.getTexture().GetData<Color>(terrainColor);
          
            Texture2D texMap = new Texture2D(Game1.GameInstamce.GraphicsDevice, (int)terrain.getContentSize().width, (int)terrain.getContentSize().height);
            texMap.SetData<Color>(terrainColor);
            CTexture pTex = new CTexture();
            pTex.setImage(texMap);
            pTex.setBatchNode(Game1.GameInstamce.spriteBatch);

            map = CSprite.create();
            map.setTexture(pTex);
            
            map.setAnchorPoint(CPoint.create(0, 0));
            map.setPosition(0, 0);
            backGround = CSprite.create(BG);
            backGround.setAnchorPoint(CPoint.create(0,0));
            backGround.setPosition(0,0);
            this.addChild(backGround);
            this.addChild(map);

            crater = CDirector.sharedDirector().getContentManager().Load<Texture2D>("mask");
            mapColor = new Color[(int)map.getContentSize().width * (int)map.getContentSize().height];
            map.getTexture().GetData(mapColor);
            mapRect = new Rectangle(0, 0, (int)map.getContentSize().width, (int)map.getContentSize().height);

            this.scheduleUpdate();
        }

        public override void update(float dt)
        {
            mapRect = new Rectangle(0, 0, (int)map.getContentSize().width, (int)map.getContentSize().height);
            base.update(dt);
        }
    }
}
