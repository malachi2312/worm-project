using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.System
{
    class CFiniteTimeAction : CAction
    {
        protected float duration;

        public CFiniteTimeAction()
        {
            duration = 0.0f;
        }

        public void setDuration(float dur)
        {
            this.duration = dur;
        }

        public float getDuration()
        {
            return this.duration;
        }
    }
}
