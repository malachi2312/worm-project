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

namespace WindowsGame2
{
    class Terrain: CNode
    {
        public CSprite map;
        public Texture2D crater;
        public Color[] mapColor;
        public Rectangle mapRect;
        CSprite backGround;
        public Terrain(String path, String BG)
        {
            map = CSprite.create(path);
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
        }

        public override void update(float dt)
        {
            mapRect = new Rectangle(0, 0, (int)map.getContentSize().width, (int)map.getContentSize().height);
            base.update(dt);
        }
    }
}
