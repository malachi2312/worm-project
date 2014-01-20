using GameFrameWork.Core;
using GameFrameWork.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.System
{
    class CActionManager : CObject
    {
        //protected List<CAction> listAction;

        protected Dictionary<CNode, List<CAction>> listAction;

        public CActionManager()
        {
            listAction = new Dictionary<CNode, List<CAction>>();

            CDirector.sharedDirector().getScheduler().scheduleWithTarget(update , 0 , 0 , 0 , 0);
        }

        public void addAction(CAction pAction, CNode pTarget, bool isPause)
        {
            pAction.startWithTarget(pTarget);


            if (listAction.ContainsKey(pTarget))
            {
                listAction[pTarget].Add(pAction);
            }
            else
            {
                List<CAction> pList = new List<CAction>();
                pList.Add(pAction);
                listAction.Add(pTarget, pList );
            }
        }

        public void removeAction(CAction pAction)
        {
            CNode pTarget = pAction.getTarget();

            if (listAction.ContainsKey(pTarget))
            {
                List<CAction> pListAction = listAction[pTarget];

                if (pListAction.Contains(pAction))
                {
                    pListAction.Remove(pAction);
                }
            }
        }

        public void removeAllActions()
        {
            listAction.Clear();
        }

        public void removeAllActionsFromTarget(CNode pTarget)
        {
            if (listAction.ContainsKey(pTarget))
            {
                List<CAction> pListAction = listAction[pTarget];
                pListAction.Clear();
            }
        }

        public void removeActionByTag(int tag , CNode pTarget)
        {
            if (listAction.ContainsKey(pTarget))
            {
                List<CAction> pListAction = listAction[pTarget];
                int size = pListAction.Count;

                for (int i = 0; i < size; ++i)
                {
                    if (pListAction[i].getTag() == tag)
                    {
                        pListAction.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public CAction getActionByTag(int tag, CNode pTarget)
        {
            if (listAction.ContainsKey(pTarget))
            {
                List<CAction> pListAction = listAction[pTarget];
                int size = pListAction.Count;

                for (int i = 0; i < size; ++i)
                {
                    if (pListAction[i].getTag() == tag)
                    {
                        return pListAction[i];
                    }
                }
            }

            return null;
        }

        public void update(float dt)
        {
            List<CNode> pListTarget = listAction.Keys.ToList();
            int sizeTarget = listAction.Keys.Count;
            if( sizeTarget <= 0 )
                return;

            List<CAction> pListAct = null;
            int i, j;

            for (i = 0; i < sizeTarget; ++i)
            {
                pListAct = listAction[pListTarget[i]];
                if (pListAct.Count > 0)
                {
                    for (j = 0; j < pListAct.Count; ++j)
                    {
                        if (!pListAct[j].isDone())
                        {
                            pListAct[j].update(dt);
                        }
                        else
                        {
                            removeAction(pListAct[j]);
                            j -= 1;
                        }
                    }
                }
                else
                {
                    listAction.Remove(pListTarget[i]);
                    pListTarget.Remove(pListTarget[i]);
                    i -= 1;
                    sizeTarget -= 1;
                }
            }
        }
    }
}
