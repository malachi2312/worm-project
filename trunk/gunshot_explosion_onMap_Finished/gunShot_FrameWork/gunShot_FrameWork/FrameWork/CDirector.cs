using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core
{
    class CDirector
    {

        private static CDirector s_Director = null;

        private CSize visibleSize;
        private CPoint origin;
        private CScene currentScene;
        private CScene nextScene;
        private bool isNextScene;

        private GraphicsDevice graphicsDevice;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ContentManager content;

        private CScheduler scheduler;
        private CActionManager actionManager;

        //XNA render state
        private SpriteSortMode sortMode;
        private BlendState blendMode;
        private SamplerState samplerMode;
        private DepthStencilState stencilMode;
        private RasterizerState rasterizerMode;

        //Delta
        private float deltaTime;

        CDirector()
        {
            sortMode = SpriteSortMode.Immediate;
            blendMode = BlendState.AlphaBlend;
            samplerMode = SamplerState.LinearClamp;
            stencilMode = DepthStencilState.Default;
            rasterizerMode = new RasterizerState();
            rasterizerMode.ScissorTestEnable = true;
            deltaTime = 0;
        }

        public static CDirector sharedDirector()
        {
            if (s_Director == null)
            {
                s_Director = new CDirector();
                if (!s_Director.initialize())
                {
                    CLog.create("Failed to create Director");
                }
            }

            return s_Director;
        }

        /// <summary>
        /// Initialize the director
        /// </summary>
        public bool initialize()
        {
            currentScene = null;
            nextScene = null;
            visibleSize = CSize.SizeZero;
            origin = CPoint.PointZero;
            isNextScene = false;

            graphics = null;
            spriteBatch = null;

            scheduler = new CScheduler();
            actionManager = new CActionManager();

            return true;
        }


        public void setGraphicsDevice(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

      

        public void setGraphicsDeviceManager(GraphicsDeviceManager pGraphics)
        {
            this.graphics = pGraphics;
        }

        public GraphicsDevice getGraphicsDevice()
        {
            return this.graphicsDevice;
        }

        public GraphicsDeviceManager getSharedGraphicsDeviceManager()
        {
            return this.graphics;
        }

        public void setContentManager( ContentManager content )
        {
            this.content = content;
        }

        public ContentManager getContentManager()
        {
            return this.content;
        }

        public CActionManager getActionManager()
        {
            return this.actionManager;
        }

        public void setSpriteBatch(SpriteBatch pBatch)
        {
            this.spriteBatch = pBatch;
        }

        public SpriteBatch getSharedSpriteBatch()
        {
            return this.spriteBatch;
        }

        public void runWithScene(CScene pScene)
        {
            currentScene = nextScene = pScene;
        }

        public void replaceScene(CScene pScene)
        {
            nextScene = pScene;
            isNextScene = true;
        }

        public CSize getVisibleSize()
        {
            return CSize.create( this.visibleSize );
        }

        public void setWinSize(CSize winSize)
        {
            this.visibleSize = winSize;
        }

        public CPoint getVisibleOrigin()
        {
            return CPoint.create( this.origin );
        }

        public void setVisibleOrigin(CPoint origin)
        {
            this.origin = origin;
        }

        public void init()
        {
            if (currentScene != null)
            {
                currentScene.init();
            }
        }

        public void update(float dt)
        {
            if (currentScene != null)
            {
                if (isNextScene)
                {
                    isNextScene = false;
                    nextScene.init();
                    currentScene.destroy();
                    currentScene = nextScene;
                }
                //currentScene.updateVisit(dt);
                deltaTime = dt;
                getScheduler().update(dt);
            }
        }

        public void beginRender()
        {
            spriteBatch.Begin(sortMode, blendMode, samplerMode, stencilMode, rasterizerMode);
        }

        public void endRender()
        {
            spriteBatch.End();
        }

        public void draw()
        {
            if (currentScene != null)
            {
                beginRender();
                currentScene.visit();
                //PhysicWorld.draw();
                endRender();
            }
        }

        public void destroy()
        {
            if (currentScene != null)
            {
                currentScene.destroy();
            }
        }

        public CScheduler getScheduler()
        {
            return scheduler;
        }

        public CScene getRunningScene()
        {
            return currentScene;
        }

        public float getDeltaTime()
        {
            return deltaTime;
        }
    }
}
