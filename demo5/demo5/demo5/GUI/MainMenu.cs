using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameFrameWork.Core.Base;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using GameFrameWork.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace demo5
{
    class MainMenu : CLayer
    {

        public static CScene scene()
        {
            CScene scene = new CScene();
            MainMenu layer = new MainMenu();
            scene.addChild(layer);

            return scene;
        }
        //Button
        Button playButton;
        Button exitButton;
        Button optionButton;
        Button aboutButton;
        Button howToPlayButton;
        //Normal
        CSprite background;
        CSprite playNormal;
        CSprite exitNormal;
        CSprite optionNormal;
        CSprite aboutNormal;
        CSprite howToPlayNormal;
        //Hover
        CSprite playHover;
        CSprite exitHover;
        CSprite optionHover;
        CSprite aboutHover;
        CSprite howToPlayHover;

        public MainMenu()
        {
            background = CSprite.create("MainMenu\\MainMenu");
            background.setPosition(500, 300);
            this.addChild(background);

            playNormal = CSprite.create("MainMenu\\menuPlay");
            playHover = CSprite.create("MainMenu\\menuPlayHover");

            exitNormal = CSprite.create("MainMenu\\menuExit");
            exitHover = CSprite.create("MainMenu\\menuExitHover");

            optionNormal = CSprite.create("MainMenu\\menuOption");
            optionHover = CSprite.create("MainMenu\\menuOptionHover");

            aboutNormal = CSprite.create("MainMenu\\menuAbout");
            aboutHover = CSprite.create("MainMenu\\menuAboutHover");

            howToPlayNormal = CSprite.create("MainMenu\\menuHowToPlay");
            howToPlayHover = CSprite.create("MainMenu\\HowToPlayHover");

            playButton = new Button(playNormal, playHover, 700, 200);
            exitButton = new Button(exitNormal, exitHover, 700, 320);
            optionButton = new Button(optionNormal, optionHover, 700, 260);
            aboutButton = new Button(aboutNormal, aboutHover, 700, 380);
            howToPlayButton = new Button(howToPlayNormal, howToPlayHover, 700, 440);


            this.addChild(playButton);
            this.addChild(exitButton);
            this.addChild(optionButton);
            this.addChild(aboutButton);
            this.addChild(howToPlayButton);

            this.scheduleUpdate();
        }

        private ButtonState mLastState = ButtonState.Released;

        public override void update(float dt)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed  )
            {
                mLastState = ButtonState.Pressed;               
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                if (mLastState == ButtonState.Pressed &&
                    playButton.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(MapMenu.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                    optionButton.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(Option.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                    aboutButton.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(About.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                    howToPlayButton.isClick == true)
                {
                    this.unscheduleUpdate();
                    CDirector.sharedDirector().replaceScene(HowToPlay.scene());
                }
                else if (mLastState == ButtonState.Pressed &&
                    exitButton.isClick == true)
                {

                }
            }
        }
    }
}
