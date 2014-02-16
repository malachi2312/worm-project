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
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
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
        Button aboutButton;
        Button howToPlayButton;
        //Normal
        CSprite background;
        CSprite playNormal;
        CSprite exitNormal;
        CSprite aboutNormal;
        CSprite howToPlayNormal;
        //Hover
        CSprite playHover;
        CSprite exitHover;
        CSprite aboutHover;
        CSprite howToPlayHover;

        Song menuMusic;

        Button3 musicButton;
        CSprite musicOn;
        CSprite musicOff;

        public static bool exit;

        public MainMenu()
        {
            background = CSprite.create("BackGroundMenu\\MainMenu");
            background.setPosition(500, 300);
            this.addChild(background);

            playNormal = CSprite.create("Button\\menuPlay");
            playHover = CSprite.create("Button\\menuPlayHover");

            exitNormal = CSprite.create("Button\\menuQuit");
            exitHover = CSprite.create("Button\\menuQuitHover");

            aboutNormal = CSprite.create("Button\\menuAbout");
            aboutHover = CSprite.create("Button\\menuAboutHover");

            howToPlayNormal = CSprite.create("Button\\menuHelp");
            howToPlayHover = CSprite.create("Button\\menuHelpHover");

            playButton = new Button(playNormal, playHover, 900, 270);
            howToPlayButton = new Button(howToPlayNormal, howToPlayHover, 900, 330);
            aboutButton = new Button(aboutNormal, aboutHover, 900, 390);
            exitButton = new Button(exitNormal, exitHover, 900, 450);

            menuMusic = CDirector.sharedDirector().getContentManager().Load<Song>("Sound\\menuMusic");

            MediaPlayer.Play(menuMusic);

            this.addChild(playButton);
            this.addChild(exitButton);
            this.addChild(aboutButton);
            this.addChild(howToPlayButton);

            musicOn = CSprite.create("Button\\musicOn");
            musicOff = CSprite.create("Button\\musicOf");
            musicButton = new Button3(musicOn, musicOff,950,550);
            this.addChild(musicButton);

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
                    exit = true;
                }
            }
        }
    }
}
