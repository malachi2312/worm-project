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
namespace gunShot_FrameWork
{
    class Terrain: CLayer
    {
        public CSprite terrain;
  
        public Color[] terrainColor;
 
        public Rectangle terrainRect;
  
        public Terrain()
        {
            terrain = CSprite.create("map");
            terrain.setAnchorPoint(CPoint.create(0,0));
            terrain.setPosition(0, 300);
            this.addChild(terrain);
            terrainColor = new Color[(int)terrain.getContentSize().width * (int)terrain.getContentSize().height];
            terrain.getTexture().GetData(terrainColor);
            terrainRect = new Rectangle(0, 300, (int)terrain.getContentSize().width, (int)terrain.getContentSize().height);
            //this.scheduleUpdate();
        }

       
        public override void update(float dt) 
        {
            terrainRect = new Rectangle(0, 300, (int)terrain.getContentSize().width, (int)terrain.getContentSize().height);
            base.update(dt);
        }

        
    }
}
