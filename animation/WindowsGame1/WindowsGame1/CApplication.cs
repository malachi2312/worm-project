using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameFrameWork.Source;
using MobileClient.Networking;
using GameFrameWork.Source.Scene;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core.Component;
using TetrisOnline.Source.Scene;

namespace GameFrameWork.Core
{
    class CApplication
    {
        private static CApplication s_Application = null;

        private CSize frameSize;
        private DisplayOrientation orientation;
        private bool isFullScreen;

        CApplication()
        {

        }

        public static CApplication sharedApplication()
        {
            if (s_Application == null)
            {
                s_Application = new CApplication();
                s_Application.initialize();
            }

            return s_Application;
        }

        public void initialize()
        {
            settingUpGraphics();
        }

        public void setting(GraphicsDeviceManager graphics, SpriteBatch pBatch , ContentManager content)
        {
            graphics.PreferredBackBufferWidth = (int)this.frameSize.width;
            graphics.PreferredBackBufferHeight = (int)this.frameSize.height;
            graphics.IsFullScreen = this.isFullScreen;
            graphics.SupportedOrientations = this.orientation;

            CDirector.sharedDirector().setGraphicsDevice(graphics);
            CDirector.sharedDirector().setSpriteBatch(pBatch);
            CDirector.sharedDirector().setWinSize(this.frameSize);
            CDirector.sharedDirector().setContentManager(content);
        }


        //These methods only use for windowns phone application
        public void settingUpGraphics()
        {
            this.isFullScreen = true;
            this.orientation = AppMacro.ORIENTATION;


            this.frameSize = CSize.create(AppMacro.WINSIZE_WIDTH, AppMacro.WINSIZE_HEIGHT);
        }


        public void init()
        {
            CDirector.sharedDirector().runWithScene(LoginScene.scene());
            CDirector.sharedDirector().init();
        }

        public void update(float dt)
        {
            NetworkController.sharedNetworkController().responseManager.visit();
            CDirector.sharedDirector().update(dt);
        }

        public void draw()
        {
            CDirector.sharedDirector().draw();
        }

        public void destroy()
        {
            CDirector.sharedDirector().destroy();
        }
    }
}
