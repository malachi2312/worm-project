using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;


namespace demo5
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            CDirector.sharedDirector().setGraphicsDeviceManager(this.graphics);
            CDirector.sharedDirector().setContentManager(this.Content);
            CDirector.sharedDirector().setGraphicsDevice(this.GraphicsDevice);


            IsMouseVisible = true;
        }


        protected override void Initialize()
        {


            base.Initialize();

            CDirector.sharedDirector().runWithScene(MainMenu.scene());
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            CDirector.sharedDirector().setSpriteBatch(this.spriteBatch);
            level = new Level();
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            CDirector.sharedDirector().update((float)gameTime.ElapsedGameTime.TotalSeconds);
            CDirector.sharedDirector().setGameTime(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            CDirector.sharedDirector().draw();
            level.drawExplosion();
            base.Draw(gameTime);
        }
    }
}
