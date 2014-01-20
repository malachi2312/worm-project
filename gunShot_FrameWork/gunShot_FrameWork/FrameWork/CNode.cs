using GameFrameWork.Core;
using GameFrameWork.Core.Component;
using GameFrameWork.Core.System;
using GameFrameWork.Core.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFrameWork.Core.Base
{
    class CNode : CObject
    {
        protected CPoint position;
        protected List<CNode> child;
        protected CNode parent;
        protected int tag;
        protected int zOrder;

        protected bool isReorderChild;
        protected bool isVisible;
        protected CSize contentSize;

        protected float scaleX;
        protected float scaleY;
        protected bool isFlipX;
        protected bool isFlipY;
        protected float rotation;
        protected float oppacity;
        protected float alpha; //0.0f - 1.0f

        public bool isNeedToTransformChild { protected set; get; }
        protected bool isNeedToParentTransform;
        protected Matrix pTransformMatrix;
        protected Matrix pLocalMatrix;

        protected CPoint anchorPoint;
        protected bool isIgnoreAnchorPointForPosition;

        //This struct support for rendering using spriteBatch
        public class relativeTransformation
        {
            public Vector2 position;
            public Vector2 scale;
            public float rotation;

            public relativeTransformation(Vector2 pos, Vector2 scale, float rot)
            {
                this.position = pos;
                this.scale = scale;
                this.rotation = rot;
            }

            public relativeTransformation()
            {
                this.position = Vector2.Zero;
                this.scale = new Vector2(1, 1);
                this.rotation = 0;
            }
            
        }

        protected relativeTransformation pRelativeTransformation;

        public CNode()
        {
            position = CPoint.PointZero;
            parent = null;
            child = new List<CNode>();
            tag = 0;
            zOrder = 0;
            isReorderChild = false;
            contentSize = CSize.SizeZero;
            isVisible = true;
            this.scaleX = this.scaleY = 1;
            this.isFlipX = this.isFlipY = false;
            this.rotation = 0;
            this.oppacity = 255;
            this.alpha = 1.0f;
            this.pTransformMatrix = Matrix.Identity;
            this.pLocalMatrix = Matrix.Identity;
            this.anchorPoint = CPoint.create(0.5f, 0.5f , 0.5f);
            this.isIgnoreAnchorPointForPosition = false;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = false;
            this.pRelativeTransformation = new relativeTransformation();
        }


        public void setPosition(CPoint p)
        {
            this.position = p;

            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public void setPosition(float x, float y)
        {
            this.setPosition(CPoint.create(x, y));
        }

        public CPoint getPosition()
        {
            //we return an copy of this position not this position
            return CPoint.create( this.position );
        }

        public void setParent(CNode pParent)
        {
            this.parent = pParent;
        }

        public CNode getParent()
        {
            return this.parent;
        }

        public List<CNode> getChildren()
        {
            return this.child;
        }

        public void setIsVisible(bool isVisible)
        {
            this.isVisible = isVisible;
        }

        public bool getIsVisible()
        {
            return this.isVisible;
        }

        public void setContentSize(CSize size)
        {
            this.contentSize = size;
        }

        public CSize getContentSize()
        {
            return CSize.create( this.contentSize );
        }


        public void setFlipX(bool isFlipX)
        {
            this.isFlipX = isFlipX;
        }

        public bool hasFlipX()
        {
            return this.isFlipX;
        }

        public void setFlipY(bool isFlipY)
        {
            this.isFlipY = isFlipY;
        }

        public bool hasFlipY()
        {
            return this.isFlipY;
        }

        public void setScale(float scale)
        {
            this.scaleX = this.scaleY = scale;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public void setScaleX(float scaleX)
        {
            this.scaleX = scaleX;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public void setScaleY(float scaleY)
        {
            this.scaleY = scaleY;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public float getScale()
        {
            if (this.scaleX != this.scaleY)
            {
                CLog.create("This node doesn't know which scale to return!");
                return 0;
            }

            return scaleX;
        }

        public float getScaleX()
        {
            return scaleX;
        }

        public float getScaleY()
        {
            return scaleY;
        }

        public void setRotation(float rotation)
        {
            this.rotation = rotation;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public float getRotation()
        {
            return this.rotation;
        }

        public void setOppacity(float oppacity)
        {
            this.oppacity = oppacity;
            this.alpha = this.oppacity / 255.0f;
            this.isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public float getOppacity()
        {
            return this.oppacity;
        }


        public CNode getChildByTag(int tag)
        {
            int size = child.Count;

            for (int i = 0; i < size; ++i)
            {
                if (child[i].tag == tag)
                {
                    return child[i];
                }
            }

            CLog.create("child tag doesn't exits");
            return null;
        }


        public void setTag(int tag)
        {
            this.tag = tag;
        }

        public int getTag()
        {
            return this.tag;
        }

        /// <summary>
        /// This method use to set the zOder like a setter
        /// Not anychange will make
        /// </summary>
        /// <param name="zOrder"></param>
        public void _setZOrder(int zOrder)
        {
            this.zOrder = zOrder;
        }

        /// <summary>
        /// This method use to set the zOrder and then reorder it in its parent
        /// </summary>
        /// <param name="zOrder"></param>
        public void setZOrder(int zOrder)
        {
            //Reorder ?
            this._setZOrder(zOrder);
            if (this.parent != null)
            {
                this.parent.setReorderChild();
            }
        }

        public int getZOrder()
        {
            return this.zOrder;
        }

        public void ignoreAnchorPointForPosition(bool value)
        {
            isIgnoreAnchorPointForPosition = value;
            isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public void setAnchorPoint(CPoint p)
        {
            this.anchorPoint = p;
            isNeedToParentTransform = true;
            this.isNeedToTransformChild = true;
        }

        public CPoint getAnchorPoint()
        {
            if (!isIgnoreAnchorPointForPosition)
                return CPoint.create( this.anchorPoint );

            return CPoint.PointZero;
        }

        

        public void addChild(CNode pNode)
        {
            if (pNode == null)
            {
                CLog.create("Child cannot NULL");
                return;
            }

            if (pNode.getParent() != null)
            {
                CLog.create("Child already has parend");
                return;
            }

            this.addChild(pNode, pNode.getZOrder(), pNode.tag);
        }

        public void addChild(CNode pNode, int zOrder)
        {
            if (pNode == null)
            {
                CLog.create("Child cannot NULL");
                return;
            }

            if (pNode.getParent() != null)
            {
                CLog.create("Child already has parend");
                return;
            }

            this.addChild(pNode, zOrder, pNode.tag);
        }

        public void addChild(CNode pNode, int zOrder, int tag)
        {
            if (pNode == null)
            {
                CLog.create("Child cannot NULL");
                return;
            }

            if (pNode.getParent() != null)
            {
                CLog.create("Child already has parent");
                return;
            }

            pNode.setZOrder(zOrder);
            pNode.tag = tag;
            pNode.setParent(this);

            child.Add(pNode);

            sortChildren(child, child.Count - 1);

        }


        protected void reorderAllChild()
        {
            if (isReorderChild)
            {
                sortChildren(child);
                isReorderChild = false;
            }
        }

        public void setReorderChild()
        {
            isReorderChild = true;
        }

        public void scheduleUpdate()
        {
            CDirector.sharedDirector().getScheduler().scheduleWithTarget( update );
        }

        public void unscheduleUpdate()
        {
            CDirector.sharedDirector().getScheduler().unscheduleWithTarget( update );
        }

        public void schedule( Core.System.sel_schedule method )
        {
            CDirector.sharedDirector().getScheduler().scheduleWithTarget( method );
        }

        public void schedule(Core.System.sel_schedule method , float interval)
        {
            CDirector.sharedDirector().getScheduler().scheduleWithTargetAndDelay(method, interval);
        }

        public void scheduleOnce(Core.System.sel_schedule method)
        {
            CDirector.sharedDirector().getScheduler().scheduleWithTargetAndRepeatCount( method, 1);
        }

        public void unschedule(Core.System.sel_schedule method)
        {
            CDirector.sharedDirector().getScheduler().unscheduleWithTarget( method );
        }

        public void removeChild(CNode pChild)
        {
            if (child.Contains(pChild))
            {
                child.Remove(pChild);
            }
            else
            {
                CLog.create("Remove child : child not found");
            }
        }

        public void removeChildByTag(int tag)
        {
            CNode children = this.getChildByTag(tag);

            if (children != null)
            {
                children.destroy();
                child.Remove(children);
            }
            else
            {
                CLog.create("Remove child by tag : child not found with tag " + tag);
            }
        }

        public void removeAllChildrenWithCleanUp()
        {
            int size = child.Count;

            for (int i = 0; i < size; ++i)
            {
                child[i].destroy();
            }

            child.Clear();
        }

        public void removeFromParentWithCleanUp()
        {
            if (this.getParent() != null)
            {
                this.getParent().removeChild(this);
                this.destroy();
                this.parent = null;
            }
        }

        virtual public bool init()
        {
            return true;
        }

        virtual public void update(float dt)
        {
            
        }

        virtual public void draw()
        {
            
        }

        virtual protected void beforeDraw()
        {
            this.nodeToParentTransform();
        }

        virtual protected void afterDraw()
        {
            if (this.isNeedToTransformChild)
                this.isNeedToTransformChild = false;
        }

        virtual public CRect boundingBox()
        {
            return CRect.create(this.position.x - this.contentSize.width * this.anchorPoint.x
                                    , this.position.y - this.contentSize.height * this.anchorPoint.y 
                                    , this.contentSize.width, this.contentSize.height);
        }

        /// <summary>
        /// Recursive all children to draw it
        /// </summary>
        virtual public void visit()
        {
            //Return if not visible 
            //Children won't be draw
            if ( !this.isVisible)
                return;

            this.beforeDraw();

            if (this.child != null && this.child.Count > 0)
            {
                this.reorderAllChild();

                //Self draw
                this.draw();

                //Recursive all children to draw them
                int size = child.Count;

                for (int i = 0; i < size; ++i)
                {
                    child[i].visit();
                }
            }
            else
            {
                this.draw();
            }


            this.afterDraw();
        }


        virtual public void destroy()
        {
            stopAllAction();
            //TODO clean up things
            if (child != null)
            {
                removeAllChildrenWithCleanUp();
            }
        }

        private void sortChildren(List<CNode> pList)
        {
            sortChildren(pList, 0);
        }

        private void sortChildren(List<CNode> pList , int start )
        {
            int pos;
            CNode temp;
            int size = pList.Count;
            for (int i = start; i < size; i++)
            {
                temp = pList[i];
                pos = i - 1;
                while ( (pos >= 0) && ( pList[pos].getZOrder() > temp.getZOrder() ) )
                {
                    pList[pos + 1] = pList[pos];
                    pos--;
                }
                pList[pos + 1] = temp;
            }
        }

        /// <summary>
        /// Transform a node to parent relative position , scale , rotation
        /// </summary>
        protected Matrix nodeToParentTransform()
        {
            if (this.isNeedToParentTransform || ( this.getParent() != null && this.getParent().isNeedToTransformChild ) )
            {
                this.isNeedToParentTransform = false;

                Matrix parentRelativeMat = Matrix.Identity;
                CPoint anchorPoint = CPoint.PointZero;

                if (this.getParent() != null)
                {
                    parentRelativeMat = this.getParent().nodeToParentTransform();
                }

                if (!isIgnoreAnchorPointForPosition)
                {
                    anchorPoint = CPoint.create(this.anchorPoint.x , this.anchorPoint.y , this.anchorPoint.z);
                }


                this.pLocalMatrix =
                    Matrix.CreateTranslation(-this.getContentSize().width * anchorPoint.x , -this.getContentSize().height * anchorPoint.y , 0) *
                    Matrix.CreateRotationZ( MathHelper.ToRadians( this.rotation ) ) *
                    Matrix.CreateScale(this.scaleX, this.scaleY, 1) *
                    Matrix.CreateTranslation(this.position.x  , this.position.y , this.position.z);

                
                this.pTransformMatrix = this.pLocalMatrix * parentRelativeMat;

                calculateRelativeTransformation(out this.pRelativeTransformation.position,
                out this.pRelativeTransformation.scale, out this.pRelativeTransformation.rotation);
            }


            return pTransformMatrix;
        }

        public CPoint convertToWorldSpace(CPoint nodePoint)
        {
            Vector3 translation = nodeToParentTransform().Translation;
            return CPoint.create(nodePoint.x + translation.X, nodePoint.y + translation.Y);
            //return CPoint.create(nodePoint.x + pTransformMatrix.Translation.X, nodePoint.y + pTransformMatrix.Translation.Y);
        }

        public CPoint convertToLocalSpace(CPoint worldPoint)
        {

            CPoint originOnWorldSpace = convertToWorldSpace(CPoint.PointZero);

            return CPoint.create( worldPoint.x - originOnWorldSpace.x , worldPoint.y - originOnWorldSpace.y );
        }


        public CAction runAction(CAction pAct)
        {
            CDirector.sharedDirector().getActionManager().addAction(pAct, this, false);
            return pAct;
        }

        public void stopAction(CAction pAction)
        {
            CDirector.sharedDirector().getActionManager().removeAction(pAction);
        }

        public void stopAllAction()
        {
            CDirector.sharedDirector().getActionManager().removeAllActionsFromTarget(this);
        }

        public void stopActionByTag(int tag)
        {
            CDirector.sharedDirector().getActionManager().removeActionByTag(tag, this);
        }

        public CAction getActionByTag(int tag)
        {
            return CDirector.sharedDirector().getActionManager().getActionByTag(tag, this);
        }

        

        protected void calculateRelativeTransformation(out Vector2 pos , out Vector2 scale , out float rotation)
        {
            Vector3 scale3;
            Vector3 pos3;
            Quaternion rot3;
            this.pTransformMatrix.Decompose(out scale3, out rot3, out pos3);

            Vector2 direction = Vector2.Transform(Vector2.UnitX, rot3);

            pos = new Vector2(pos3.X, pos3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
            rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);
        }

        //This is just for test
        public void debugMatrix(string content , Matrix m)
        {
            CLog.create(content + "\n" +
                m.M11 + " " + m.M12 + " " + m.M13 + " " + m.M14 + "\n" +
                m.M21 + " " + m.M22 + " " + m.M23 + " " + m.M24 + "\n" +
                m.M31 + " " + m.M32 + " " + m.M33 + " " + m.M34 + "\n" +
                m.M41 + " " + m.M42 + " " + m.M43 + " " + m.M44 + "\n");
        }
    }
}
