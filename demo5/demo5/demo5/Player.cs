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

namespace demo5
{
    class Player:CNode
    {
        public CSprite playerTexture;
        CAnimationSprite playerAnimation;
        CAnimation move; 
        CAnimation idle; 

        float playerScaling;

        public Vector2 velocity;

        public Rectangle playerRectangle;

        public Vector2 playerPosition = new Vector2(100,200);

        public Color[] playerColorData;

        public Color[,] playerColorArray;

        public Player(Terrain map)
        {

            SetUpPlayer();
            playerColorArray = map.TextureTo2DArray(playerTexture.getTexture());
            
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
        }

        public void UpdateRect()
        {
            playerPosition.Y = (float)Math.Floor((playerPosition.Y));
            playerAnimation.setPosition(playerPosition.X, playerPosition.Y);
            playerRectangle = new Rectangle((int)playerAnimation.getPosition().x, (int)playerAnimation.getPosition().y,
                (int)playerAnimation.getContentSize().width, (int)(playerAnimation.getContentSize().height / 3.5f));
        }

        public void UpdatePlayer(float dt)
        {

            ControlPlayers();
        }

        void ControlPlayers()
        {
            playerPosition.Y += velocity.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                playerPosition.X -= 0.5f;
                if (!move.isRunning)
                {
                    playerAnimation.getAnimator().play("move");
                    playerAnimation.setFlipY(false);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

                playerAnimation.setFlipY(true);
                playerPosition.X += 0.5f;
                if (!move.isRunning)
                {
                    playerAnimation.getAnimator().play("move");
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {

                if (!idle.isRunning)
                    playerAnimation.getAnimator().play("idle");
            }

            velocity.Y += 0.15f * 0.75f;

            UpdateRect();

            if (playerPosition.Y > 500 - playerAnimation.getContentSize().height / 2)
            {
                playerPosition.Y = 500 - playerAnimation.getContentSize().height / 2;
                velocity.Y = 0;
            }
        }
    }
}
