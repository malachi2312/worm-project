using GameFrameWork.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.System
{
    public delegate void sel_schedule ( float dt );

    class CScheduler
    {
        private class CScheduleMap
        {
            public CScheduleMap( sel_schedule target , float delay , float life , int repeatCount , int priority )
            {
                this.target = target;
                this.delayTime = delay;
                this.lifeTime = life;
                this.counter = 0;
                this.repeatCount = repeatCount;
                this.timeCounter = delay;
                this.priority = priority;
            }

            public sel_schedule target;
            public float delayTime;
            public float lifeTime;
            public int repeatCount;

            public float timeCounter;
            public float counter;
            public int priority;
        }

        //private List<CScheduleMap> listUpdate;
        private IteratorList<CScheduleMap> listUpdate;

        public CScheduler()
        {
            listUpdate = new IteratorList<CScheduleMap>();
        }

        public void scheduleWithTarget(sel_schedule method)
        {
            this.scheduleWithTarget(method , 0 , 0 , 0 , 0 );
        }

        public void scheduleWithTargetAndDelay( sel_schedule method , float delay )
        {
            this.scheduleWithTarget(method , delay , 0 , 0 , 0 );
        }


        public void scheduleWithTargetAndTimeLife( sel_schedule method , float life )
        {
            this.scheduleWithTarget(method , 0 , life , 0 , 0 );
        }


        public void scheduleWithTargetAndDelayAndTimeLife( sel_schedule method , float delay , float life )
        {
            this.scheduleWithTarget(method , delay , life , 0 , 0);
        }


        public void scheduleWithTargetAndRepeatCount(sel_schedule method, int count)
        {
            this.scheduleWithTarget(method, 0, 0, count , 0);
        }

        public void scheduleWithTarget(sel_schedule method , float delay , float lifeTime , int repeatCount , int priority )
        {
            CScheduleMap map = new CScheduleMap(method , delay , lifeTime , repeatCount , priority );

            if (listUpdate.Contains(map))
            {
                CLog.create("Method aldready scheduled");
                return;
            }

            listUpdate.addItem(map);

            sortChildren(listUpdate, listUpdate.Size() - 1);
        }

        public void unscheduleWithTarget(sel_schedule method  )
        {
            int size = listUpdate.Size();

            for (int i = 0; i < size; ++i)
            {
                if (listUpdate[i].target == method)
                {
                    listUpdate.removeItemAtIndex(i);
                    break;
                }
            }
        }


        public void update(float dt)
        {
            for (int i = listUpdate.begin(); i < listUpdate.end(); i = listUpdate.next())
            {
                CScheduleMap map = listUpdate[i];

                if (map.target == null)
                {
                    listUpdate.removeItemAtIndex(i);
                    continue;
                }

                if ( map.repeatCount == 0 )
                {
                    map.timeCounter += dt;
                    if (map.timeCounter >= map.delayTime )
                    {
                        map.target.Invoke(dt);

                        if (map.timeCounter >= map.lifeTime && map.lifeTime != 0)
                        {
                            listUpdate.removeItem(map);
                        }

                        map.timeCounter -= map.delayTime;
                    }
                }
                else
                {
                    map.target.Invoke(dt);

                    map.counter += 1;
                    if (map.counter >= map.repeatCount)
                    {
                        listUpdate.removeItem(map);
                    }
                }
            }
        }

        private void sortChildren(IteratorList<CScheduleMap> pList, int start)
        {
            int pos;
            CScheduleMap temp;
            int size = pList.Size();
            for (int i = start; i < size; i++)
            {
                temp = pList[i];
                pos = i - 1;
                while ((pos >= 0) && (pList[pos].priority > temp.priority))
                {
                    pList[pos + 1] = pList[pos];
                    pos--;
                }
                pList[pos + 1] = temp;
            }
        }
    }
}
