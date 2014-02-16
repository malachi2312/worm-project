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
    class Worm : CNode
    {
        public CSprite playerTexture;
        CSprite playerName;
        CAnimationSprite playerAnimation;
        CAnimation move;
        CAnimation idle;

        float playerScaling;

        public Vector2 velocity;

        /// <summary>
        /// rect check on terraint
        /// </summary>
        public Rectangle playerRectangle1;
        /// <summary>
        /// rect check bullet hit
        /// </summary>
        public Rectangle playerRectangle2;
        /// <summary>
        /// rect check right
        /// </summary>
        public Rectangle playerRectangle3;
        /// <summary>
        /// rect check left
        /// </summary>
        public Rectangle playerRectangle4; 
        
        public Vector2 playerPosition = new Vector2(100, 0);

        public Color[] playerColorData;

        public bool isMoveAble = true;

        int fullHealth;
        public int healthPoint;
        public float movePoint;
        int fullMove;

        CSprite healthBar1;
        CSprite healthBar2;

        //public CSprite onTurn;

        public Worm(string path)
        {
            SetUpPlayer();
            playerName = CSprite.create(path);
            playerName.setAnchorPoint(CPoint.create(0, 0));
            playerName.setPosition(-10, 70);
            playerAnimation.addChild(playerName);
        }

        void SetUpPlayer()
        {
            playerAnimation = CAnimationSprite.create("worm_sheet", 64, 64);
            move = CAnimation.create(64, 64, 3, 0, 2, false, 0.2f);
            idle = CAnimation.create(64, 64, 3, 0, 0, false, 0.2f);

            playerTexture = CSprite.create("worm_sheet");

            playerAnimation.setPosition(CPoint.create(playerPosition.X, playerPosition.Y));
            playerScaling = 40f / playerAnimation.getContentSize().width;
            playerAnimation.setScale(playerScaling);
            playerColorData = new Color[(int)(playerTexture.getContentSize().width * playerTexture.getContentSize().height)];
            this.scheduleUpdate();

            playerTexture.getTexture().GetData(playerColorData);

            playerAnimation.getAnimator().addAnimation("move", move);
            playerAnimation.getAnimator().addAnimation("idle", idle);

            this.addChild(playerAnimation);

            //HP
            healthBar1 = CSprite.create("healthBar");
            healthBar1.setAnchorPoint(CPoint.create(0, 0));
            healthBar1.setPosition(10, 70);

            healthBar2 = CSprite.create("healthBar");
            healthBar2.setAnchorPoint(CPoint.create(0, 0));
            healthBar2.setColor(Color.Red);
            healthBar2.setPosition(10, 70);

            fullHealth = 100;
            healthPoint = 100;

            fullMove = 30;
            movePoint = 30;


            playerAnimation.addChild(healthBar1);
            playerAnimation.addChild(healthBar2);
        }

        public CPoint getBulletStartPosition()
        {
            CPoint point = CPoint.create(playerPosition.X, playerPosition.Y);
            return point;
        }

        public void UpdateRect()
        {
            playerPosition.Y = (float)Math.Floor((playerPosition.Y));
            playerAnimation.setPosition(playerPosition.X, playerPosition.Y);

            playerRectangle1 = new Rectangle((int)playerAnimation.getPosition().x - 12, (int)playerAnimation.getPosition().y,
                (int)playerAnimation.getContentSize().width, (int)(playerAnimation.getContentSize().height / 3.5f));

            playerRectangle2 = new Rectangle((int)playerPosition.X - (int)(playerAnimation.getContentSize().width/2f) + 4, (int)playerPosition.Y - 30,
                (int)(playerAnimation.getContentSize().width)  , (int)(playerAnimation.getContentSize().height - 15));

            playerRectangle3 = new Rectangle((int)playerPosition.X , (int)playerPosition.Y - 35,
                (int)(playerAnimation.getContentSize().width -  30), (int)(playerAnimation.getContentSize().height / 2f));
            playerRectangle4 = new Rectangle((int)(playerPosition.X - playerAnimation.getContentSize().width / 2f), (int)playerPosition.Y - 35,
                (int)(playerAnimation.getContentSize().width - 30), (int)(playerAnimation.getContentSize().height / 2f));
        }

        public void UpdatePlayer(float dt)
        {
            getBulletStartPosition();

            if (isMoveAble)
            {
                ControlPlayers();
            }

            healthControl();

            playerPosition.Y += velocity.Y;

            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {

                if (!idle.isRunning)
                    playerAnimation.getAnimator().play("idle");
            }

            velocity.Y += 0.15f * 0.75f;

            UpdateRect();
        }

        public bool isLeft, isRight, setWall;
        void ControlPlayers()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                playerPosition.X -= 0.5f;
                movePoint -= 0.2f;
                if (playerPosition.X < 20)
                    playerPosition.X = 20;
                if (!move.isRunning)
                {
                    playerAnimation.getAnimator().play("move");
                    playerAnimation.setFlipY(false);
                }

                isLeft = true;
                isRight = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                playerAnimation.setFlipY(true);
                playerPosition.X += 0.5f;
                movePoint -= 0.2f;
                if (playerPosition.X > 980)
                    playerPosition.X = 980;
                if (!move.isRunning)
                {
                    playerAnimation.getAnimator().play("move");
                }

                isLeft = false;
                isRight = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
                setWall = true;
            else
                setWall = false;

            if (movePoint < 0)
            {
                movePoint = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))                
                    playerPosition.X += 0.5f;
                if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
                    playerPosition.X -= 0.5f;
            }
        }

        void healthControl()
        {
            float healthPercent = (float)healthPoint / fullHealth;
            healthBar2.setScaleX(healthPercent);
        }
    }
}
