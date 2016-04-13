using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOlian.Collision
{
    abstract class RectCollisionCalc
    {
        private BoundingBox cb;

        protected void Init(BoundingBox _cb)
        {
            cb = _cb;
        }

        public bool Intersects(BoundingBox target)
        {
            if (cb.getBoundingBox().Intersects(target.getBoundingBox()))
                if (cb.getBoundinType() == target.getBoundinType())
                {
                    if (cb.getBoundinType() == boundingType.AABB)
                        return true;
                    else if (cb.getBoundinType() == boundingType.AIDBC)
                    {
                        return Math.Abs(Vector2.Distance(cb.getCenter(), target.getCenter())) <= (cb.getSize().X + target.getSize().X)/2;
                    }
                }
                else
                {
                }
            return false;
        }

        #region Directional Detection
        public bool isLeft(BoundingBox target)
        {
            Rectangle rect = Rectangle.Intersect(cb.getBoundingBox(), target.getBoundingBox());
            if (this.Intersects(target) ? cb.getBoundingBox().Left == rect.Left : false)
                return rect.Height > rect.Width;
            return false;
        }
        public bool isRight(BoundingBox target)
        {
            Rectangle rect = Rectangle.Intersect(cb.getBoundingBox(), target.getBoundingBox());
            if (this.Intersects(target) ? cb.getBoundingBox().Right == rect.Right : false)
                return rect.Height > rect.Width;
            return false;
        }
        public bool isAbove(BoundingBox target)
        {
            Rectangle rect = Rectangle.Intersect(cb.getBoundingBox(), target.getBoundingBox());
            if (this.Intersects(target) ? cb.getBoundingBox().Top == rect.Top : false)
                return rect.Height < rect.Width;
            return false;
        }
        public bool isBelow(BoundingBox target)
        {
            Rectangle rect = Rectangle.Intersect(cb.getBoundingBox(), target.getBoundingBox());
            if (this.Intersects(target) ? cb.getBoundingBox().Bottom == rect.Bottom : false)
                return rect.Height < rect.Width;
            return false;
        }
        #endregion

        public double degreeTo(BoundingBox target)
        {
            double deg = MathHelper.ToDegrees((float)(Math.Atan2(target.getCenter().X - cb.getCenter().X, target.getCenter().Y - cb.getCenter().Y) - Math.PI / 2));
            return deg < 0 ? deg + 360 : deg > 360 ? deg - 360 : deg;
        }
        public double radiantTo(BoundingBox target)
        {
            double deg = Math.Atan2(target.getCenter().X - cb.getCenter().X, target.getCenter().Y - cb.getCenter().Y ) - Math.PI / 2;
            return deg < 0 ? deg + Math.PI * 2 : deg > Math.PI * 2 ? deg - Math.PI * 2 : deg;
        }
    }
}
