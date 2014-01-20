using GameFrameWork.Core;
using GameFrameWork.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Base
{
    class CScene : CNode
    {


        public CScene()
        {

        }

        override public bool init()
        {
            bool bRet = false;
            do
            {
                if (CDirector.sharedDirector() == null)
                {
                    CLog.create("Failed to get director/nCannot create new scene");
                    break;
                }

                base.init();

                this.setContentSize(CDirector.sharedDirector().getVisibleSize());
                this.ignoreAnchorPointForPosition(true);

                bRet = true;
            } while (false);

            return bRet;
        }

        public static CScene create()
        {
            CScene pScene = new CScene();
            if (pScene != null && pScene.init())
                return pScene;

            return null;
        }
    }
}
