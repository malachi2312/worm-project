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
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class Bullet : CNode
    {
        public CSprite bullet;

        public float bulletScaling = 0.12f;

        public float bulletAngle;
        public bool bulletFlying = false;
        public Vector2 bulletPosition;
        public Color[] bulletColorData;
        public Rectangle bulletRectangle;
        public double Vx, Vy;

        protected double g = -9.8;
        public float scaleV = 10;
        protected float angle;

        protected int damage;
        public int Damage
        {
            get { return damage; }
        }

        public virtual void Explosion()
        {
        }
    }
}
